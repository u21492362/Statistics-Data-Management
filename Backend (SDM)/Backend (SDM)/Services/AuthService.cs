using BCrypt.Net;
using Backend__SDM_.Models.ViewModels.Auth;
using Backend__SDM_.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Backend__SDM_.Models.Data;
using Backend__SDM_.ViewModels.Auth;
using Backend__SDM_.Models.Enums;
using Backend__SDM_.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Backend__SDM_.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            var email = request.Email.Trim().ToLower();

            var exists = await _context.AppUsers.AnyAsync(x => x.Email.ToLower() == email && !x.IsDeleted);
            if (exists)
            {
                throw new InvalidOperationException("An account with this email already exists.");
            }

            var user = new AppUser
            {
                FullName = request.FullName.Trim(),
                Email = email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = UserRole.DataCapturer,
                CircuitId = request.CircuitId,
                SocietyId = request.SocietyId,
                IsActive = true
            };

            _context.AppUsers.Add(user);
            await _context.SaveChangesAsync();

            return GenerateAuthResponse(user);
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var email = request.Email.Trim().ToLower();

            var user = await _context.AppUsers
                .FirstOrDefaultAsync(x => x.Email.ToLower() == email && !x.IsDeleted);

            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            if (!user.IsActive)
            {
                throw new UnauthorizedAccessException("This account is inactive.");
            }

            var passwordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

            if (!passwordValid)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            return GenerateAuthResponse(user);
        }

        private AuthResponse GenerateAuthResponse(AppUser user)
        {
            var key = _configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT key is missing.");
            var issuer = _configuration["Jwt:Issuer"] ?? "MethodistStats";
            var audience = _configuration["Jwt:Audience"] ?? "MethodistStatsUsers";
            var expiresInMinutes = int.Parse(_configuration["Jwt:ExpiresInMinutes"] ?? "120");

            var expires = DateTime.UtcNow.AddMinutes(expiresInMinutes);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            if (user.CircuitId.HasValue)
            {
                claims.Add(new Claim("CircuitId", user.CircuitId.Value.ToString()));
            }

            if (user.SocietyId.HasValue)
            {
                claims.Add(new Claim("SocietyId", user.SocietyId.Value.ToString()));
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expires,
                signingCredentials: credentials
            );

            return new AuthResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresAtUtc = expires,
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role.ToString(),
                CircuitId = user.CircuitId,
                SocietyId = user.SocietyId
            };
        }
    }
}