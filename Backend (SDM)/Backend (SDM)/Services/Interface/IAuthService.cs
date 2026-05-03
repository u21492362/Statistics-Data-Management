using Backend__SDM_.Models.ViewModels.Auth;
using Backend__SDM_.ViewModels.Auth;

namespace Backend__SDM_.Services.Interface
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(RegisterRequest request);
        Task<AuthResponse> LoginAsync(LoginRequest request);
    }
}