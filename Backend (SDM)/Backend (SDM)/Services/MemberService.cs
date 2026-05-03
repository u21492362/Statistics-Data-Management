using Backend__SDM_.Entities;
using Backend__SDM_.Models.Data;
using Backend__SDM_.Models.Enums;
using Backend__SDM_.Models.ViewModels;
using Backend__SDM_.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Backend__SDM_.Services
{
    public class MemberService : IMemberService
    {
        private readonly ApplicationDbContext _context;

        public MemberService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<MemberViewModel>> GetAllAsync(int? societyId = null, string? search = null)
        {
            var query = _context.Members
                .Include(x => x.Society)
                .Where(x => !x.IsDeleted)
                .AsQueryable();

            if (societyId.HasValue)
            {
                query = query.Where(x => x.SocietyId == societyId.Value);
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();
                query = query.Where(x =>
                    x.FullName.Contains(search) ||
                    x.FirstName.Contains(search) ||
                    x.LastName.Contains(search) ||
                    x.MembershipNumber.Contains(search));
            }

            var members = await query
                .OrderBy(x => x.FullName)
                .ToListAsync();

            return members.Select(x => new MemberViewModel
            {
                Id = x.Id,
                MembershipNumber = x.MembershipNumber,
                FirstName = x.FirstName,
                LastName = x.LastName,
                FullName = x.FullName,
                DateOfBirth = x.DateOfBirth,
                Gender = x.Gender,
                MobileNumber = x.MobileNumber,
                PhysicalAddress = x.PhysicalAddress,
                SocietyId = x.SocietyId,
                SocietyName = x.Society != null ? x.Society.Name : null,
                IsActive = x.IsActive,
                Age = x.Age
            }).ToList();
        }

        public async Task<MemberViewModel?> GetByIdAsync(int id)
        {
            var x = await _context.Members
                .Include(m => m.Society)
                .FirstOrDefaultAsync(m => m.Id == id && !m.IsDeleted);

            if (x == null) return null;

            return new MemberViewModel
            {
                Id = x.Id,
                MembershipNumber = x.MembershipNumber,
                FirstName = x.FirstName,
                LastName = x.LastName,
                FullName = x.FullName,
                DateOfBirth = x.DateOfBirth,
                Gender = x.Gender,
                MobileNumber = x.MobileNumber,
                PhysicalAddress = x.PhysicalAddress,
                SocietyId = x.SocietyId,
                SocietyName = x.Society?.Name,
                IsActive = x.IsActive,
                Age = x.Age
            };
        }

        public async Task<int> CreateAsync(MemberViewModel model)
        {
            var fullName = $"{model.FirstName} {model.LastName}".Trim();

            var entity = new Member
            {
                MembershipNumber = model.MembershipNumber.Trim(),
                FirstName = model.FirstName.Trim(),
                LastName = model.LastName.Trim(),
                FullName = fullName,
                DateOfBirth = model.DateOfBirth,
                Gender = model.Gender,
                MobileNumber = model.MobileNumber?.Trim(),
                PhysicalAddress = model.PhysicalAddress?.Trim(),
                SocietyId = model.SocietyId,
                IsActive = model.IsActive
            };

            _context.Members.Add(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task<bool> UpdateAsync(int id, MemberViewModel model)
        {
            var entity = await _context.Members.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
            if (entity == null) return false;

            entity.MembershipNumber = model.MembershipNumber.Trim();
            entity.FirstName = model.FirstName.Trim();
            entity.LastName = model.LastName.Trim();
            entity.FullName = $"{model.FirstName} {model.LastName}".Trim();
            entity.DateOfBirth = model.DateOfBirth;
            entity.Gender = model.Gender;
            entity.MobileNumber = model.MobileNumber?.Trim();
            entity.PhysicalAddress = model.PhysicalAddress?.Trim();
            entity.SocietyId = model.SocietyId;
            entity.IsActive = model.IsActive;
            entity.ModifiedOnUtc = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Members.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
            if (entity == null) return false;

            entity.IsDeleted = true;
            entity.ModifiedOnUtc = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
