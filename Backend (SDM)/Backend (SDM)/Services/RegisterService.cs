using Backend__SDM_.Entities;
using Backend__SDM_.Models.Data;
using Backend__SDM_.Models.Enums;
using Backend__SDM_.Models.ViewModels;
using Backend__SDM_.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Backend__SDM_.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly ApplicationDbContext _context;

        public RegisterService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<StatisticalRegisterViewModel>> GetRegistersAsync(int? yearId = null, int? societyId = null)
        {
            var query = _context.StatisticalRegisters
                .Include(x => x.StatisticalYear)
                .Include(x => x.District)
                .Include(x => x.Circuit)
                .Include(x => x.Society)
                .Include(x => x.CompiledByUser)
                .Where(x => !x.IsDeleted)
                .AsQueryable();

            if (yearId.HasValue)
            {
                query = query.Where(x => x.StatisticalYearId == yearId.Value);
            }

            if (societyId.HasValue)
            {
                query = query.Where(x => x.SocietyId == societyId.Value);
            }

            return await query
                .OrderByDescending(x => x.StatisticalYear!.Year)
                .ThenBy(x => x.Society!.Name)
                .Select(x => new StatisticalRegisterViewModel
                {
                    Id = x.Id,
                    StatisticalYearId = x.StatisticalYearId,
                    Year = x.StatisticalYear != null ? x.StatisticalYear.Year : 0,
                    DistrictId = x.DistrictId,
                    DistrictName = x.District != null ? x.District.Name : null,
                    CircuitId = x.CircuitId,
                    CircuitName = x.Circuit != null ? x.Circuit.Name : null,
                    SocietyId = x.SocietyId,
                    SocietyName = x.Society != null ? x.Society.Name : null,
                    CompiledByUserId = x.CompiledByUserId,
                    CompiledByUserName = x.CompiledByUser != null ? x.CompiledByUser.FullName : null,
                    DateCompiled = x.DateCompiled,
                    Status = x.Status,
                    Notes = x.Notes
                })
                .ToListAsync();
        }

        public async Task<StatisticalRegisterViewModel?> GetRegisterAsync(int id)
        {
            var x = await _context.StatisticalRegisters
                .Include(r => r.StatisticalYear)
                .Include(r => r.District)
                .Include(r => r.Circuit)
                .Include(r => r.Society)
                .Include(r => r.CompiledByUser)
                .FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted);

            if (x == null) return null;

            return new StatisticalRegisterViewModel
            {
                Id = x.Id,
                StatisticalYearId = x.StatisticalYearId,
                Year = x.StatisticalYear?.Year ?? 0,
                DistrictId = x.DistrictId,
                DistrictName = x.District?.Name,
                CircuitId = x.CircuitId,
                CircuitName = x.Circuit?.Name,
                SocietyId = x.SocietyId,
                SocietyName = x.Society?.Name,
                CompiledByUserId = x.CompiledByUserId,
                CompiledByUserName = x.CompiledByUser?.FullName,
                DateCompiled = x.DateCompiled,
                Status = x.Status,
                Notes = x.Notes
            };
        }

        public async Task<int> CreateRegisterAsync(StatisticalRegisterViewModel model)
        {
            var exists = await _context.StatisticalRegisters.AnyAsync(x =>
                !x.IsDeleted &&
                x.StatisticalYearId == model.StatisticalYearId &&
                x.SocietyId == model.SocietyId);

            if (exists)
            {
                throw new InvalidOperationException("A register already exists for this society and year.");
            }

            var entity = new StatisticalRegister
            {
                StatisticalYearId = model.StatisticalYearId,
                DistrictId = model.DistrictId,
                CircuitId = model.CircuitId,
                SocietyId = model.SocietyId,
                CompiledByUserId = model.CompiledByUserId,
                DateCompiled = model.DateCompiled == default ? DateTime.UtcNow : model.DateCompiled,
                Status = RegisterStatus.Draft,
                Notes = model.Notes
            };

            _context.StatisticalRegisters.Add(entity);
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task<bool> SubmitRegisterAsync(int id)
        {
            var register = await _context.StatisticalRegisters.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
            if (register == null) return false;

            register.Status = RegisterStatus.Submitted;
            register.ModifiedOnUtc = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> FinaliseRegisterAsync(int id)
        {
            var register = await _context.StatisticalRegisters.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
            if (register == null) return false;

            register.Status = RegisterStatus.Finalised;
            register.ModifiedOnUtc = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<RegisterCaptureViewModel?> GetRegisterCaptureAsync(int registerId)
        {
            var register = await _context.StatisticalRegisters
                .Include(x => x.StatisticalYear)
                .Include(x => x.District)
                .Include(x => x.Circuit)
                .Include(x => x.Society)
                .Include(x => x.CompiledByUser)
                .FirstOrDefaultAsync(x => x.Id == registerId && !x.IsDeleted);

            if (register == null) return null;

            var entries = await _context.RegisterMemberEntries
                .Include(x => x.Member)
                .Include(x => x.Categories)
                    .ThenInclude(x => x.StatisticalCategory)
                .Where(x => x.StatisticalRegisterId == registerId && !x.IsDeleted)
                .OrderBy(x => x.RowNumber)
                .ToListAsync();

            var availableMembers = await _context.Members
                .Where(x => !x.IsDeleted && x.IsActive && x.SocietyId == register.SocietyId)
                .OrderBy(x => x.FullName)
                .Select(x => new DropdownViewModel
                {
                    Id = x.Id,
                    Name = $"{x.FullName} ({x.MembershipNumber})"
                })
                .ToListAsync();

            var categories = await _context.StatisticalCategories
                .Where(x => !x.IsDeleted)
                .OrderBy(x => x.DisplayOrder)
                .ToListAsync();

            var vm = new RegisterCaptureViewModel
            {
                Register = new StatisticalRegisterViewModel
                {
                    Id = register.Id,
                    StatisticalYearId = register.StatisticalYearId,
                    Year = register.StatisticalYear?.Year ?? 0,
                    DistrictId = register.DistrictId,
                    DistrictName = register.District?.Name,
                    CircuitId = register.CircuitId,
                    CircuitName = register.Circuit?.Name,
                    SocietyId = register.SocietyId,
                    SocietyName = register.Society?.Name,
                    CompiledByUserId = register.CompiledByUserId,
                    CompiledByUserName = register.CompiledByUser?.FullName,
                    DateCompiled = register.DateCompiled,
                    Status = register.Status,
                    Notes = register.Notes
                },
                AvailableMembers = availableMembers,
                AvailableCategories = categories
                    .Select(x => new DropdownViewModel { Id = x.Id, Name = x.Name })
                    .ToList(),
                Entries = entries.Select(entry => new RegisterMemberEntryViewModel
                {
                    Id = entry.Id,
                    StatisticalRegisterId = entry.StatisticalRegisterId,
                    MemberId = entry.MemberId,
                    RowNumber = entry.RowNumber,
                    MembershipNumber = entry.Member?.MembershipNumber ?? string.Empty,
                    MemberFullName = entry.Member?.FullName ?? string.Empty,
                    DateOfBirth = entry.Member?.DateOfBirth ?? DateTime.MinValue,
                    Gender = entry.Member?.Gender ?? Gender.Unknown,
                    Age = entry.Member?.Age ?? 0,
                    Remarks = entry.Remarks,
                    Categories = categories.Select(c =>
                    {
                        var selected = entry.Categories.FirstOrDefault(ec => ec.StatisticalCategoryId == c.Id);

                        return new RegisterMemberCategoryViewModel
                        {
                            Id = selected?.Id ?? 0,
                            StatisticalCategoryId = c.Id,
                            CategoryCode = c.Code,
                            CategoryName = c.Name,
                            Selected = selected?.ValueBool ?? false,
                            ValueNumber = selected?.ValueNumber,
                            ValueText = selected?.ValueText,
                            IsSystemGenerated = c.IsSystemGenerated
                        };
                    }).ToList()
                }).ToList()
            };

            return vm;
        }

        public async Task<int> AddMemberToRegisterAsync(int registerId, int memberId, string? remarks)
        {
            var register = await _context.StatisticalRegisters
                .Include(x => x.StatisticalYear)
                .FirstOrDefaultAsync(x => x.Id == registerId && !x.IsDeleted);

            if (register == null)
            {
                throw new InvalidOperationException("Register not found.");
            }

            var member = await _context.Members.FirstOrDefaultAsync(x => x.Id == memberId && !x.IsDeleted);
            if (member == null)
            {
                throw new InvalidOperationException("Member not found.");
            }

            var exists = await _context.RegisterMemberEntries.AnyAsync(x =>
                !x.IsDeleted &&
                x.StatisticalRegisterId == registerId &&
                x.MemberId == memberId);

            if (exists)
            {
                throw new InvalidOperationException("Member already exists in this register.");
            }

            var nextRow = 1;
            var currentMax = await _context.RegisterMemberEntries
                .Where(x => x.StatisticalRegisterId == registerId && !x.IsDeleted)
                .MaxAsync(x => (int?)x.RowNumber);

            if (currentMax.HasValue)
            {
                nextRow = currentMax.Value + 1;
            }

            var entry = new RegisterMemberEntry
            {
                StatisticalRegisterId = registerId,
                MemberId = memberId,
                RowNumber = nextRow,
                Remarks = remarks
            };

            _context.RegisterMemberEntries.Add(entry);
            await _context.SaveChangesAsync();

            await CreateSystemGeneratedCategoriesAsync(entry.Id, member, register.StatisticalYear!.Year);

            return entry.Id;
        }

        public async Task<bool> SaveEntryCategoriesAsync(int registerEntryId, List<RegisterMemberCategoryViewModel> categories)
        {
            var entry = await _context.RegisterMemberEntries
                .FirstOrDefaultAsync(x => x.Id == registerEntryId && !x.IsDeleted);

            if (entry == null) return false;

            var dbCategories = await _context.StatisticalCategories
                .Where(x => !x.IsDeleted)
                .ToListAsync();

            var existing = await _context.RegisterMemberCategories
                .Where(x => x.RegisterMemberEntryId == registerEntryId && !x.IsDeleted)
                .ToListAsync();

            foreach (var item in categories)
            {
                var category = dbCategories.FirstOrDefault(x => x.Id == item.StatisticalCategoryId);
                if (category == null) continue;

                if (category.IsSystemGenerated)
                {
                    continue;
                }

                var match = existing.FirstOrDefault(x => x.StatisticalCategoryId == item.StatisticalCategoryId);

                if (item.Selected)
                {
                    if (match == null)
                    {
                        _context.RegisterMemberCategories.Add(new RegisterMemberCategory
                        {
                            RegisterMemberEntryId = registerEntryId,
                            StatisticalCategoryId = item.StatisticalCategoryId,
                            ValueBool = true,
                            ValueNumber = item.ValueNumber,
                            ValueText = item.ValueText
                        });
                    }
                    else
                    {
                        match.ValueBool = true;
                        match.ValueNumber = item.ValueNumber;
                        match.ValueText = item.ValueText;
                        match.ModifiedOnUtc = DateTime.UtcNow;
                    }
                }
                else
                {
                    if (match != null)
                    {
                        _context.RegisterMemberCategories.Remove(match);
                    }
                }
            }

            await _context.SaveChangesAsync();
            return true;
        }

        private async Task CreateSystemGeneratedCategoriesAsync(int registerEntryId, Member member, int reportingYear)
        {
            var categories = await _context.StatisticalCategories
                .Where(x => x.IsSystemGenerated && !x.IsDeleted)
                .ToListAsync();

            var age = reportingYear - member.DateOfBirth.Year;
            var birthdayInYear = new DateTime(reportingYear, member.DateOfBirth.Month, member.DateOfBirth.Day);
            var referenceDate = new DateTime(reportingYear, 12, 31);

            if (birthdayInYear > referenceDate)
            {
                age--;
            }

            string? categoryCode = null;

            if (member.Gender == Gender.Male && age > 18)
                categoryCode = "MEN_ABOVE_18";
            else if (member.Gender == Gender.Female && age > 18)
                categoryCode = "WOMEN_ABOVE_18";
            else if (member.Gender == Gender.Male && age >= 6 && age <= 18)
                categoryCode = "BOYS_6_18";
            else if (member.Gender == Gender.Female && age >= 6 && age <= 18)
                categoryCode = "GIRLS_6_18";
            else if (member.Gender == Gender.Male && age < 5)
                categoryCode = "INFANT_BOYS_UNDER_5";
            else if (member.Gender == Gender.Female && age < 5)
                categoryCode = "INFANT_GIRLS_UNDER_5";

            if (string.IsNullOrWhiteSpace(categoryCode))
                return;

            var category = categories.FirstOrDefault(x => x.Code == categoryCode);
            if (category == null)
                return;

            _context.RegisterMemberCategories.Add(new RegisterMemberCategory
            {
                RegisterMemberEntryId = registerEntryId,
                StatisticalCategoryId = category.Id,
                ValueBool = true
            });

            await _context.SaveChangesAsync();
        }
    }
}
