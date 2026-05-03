using Backend__SDM_.Entities;
using Backend__SDM_.Models.Data;
using Backend__SDM_.Models.Enums;
using Backend__SDM_.Models.ViewModels;
using Backend__SDM_.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Backend__SDM_.Services
{
    public class LookupService : ILookupService
    {
        private readonly ApplicationDbContext _context;

        public LookupService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<DropdownViewModel>> GetDistrictsAsync()
        {
            return await _context.Districts
                .Where(x => !x.IsDeleted)
                .OrderBy(x => x.Name)
                .Select(x => new DropdownViewModel
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToListAsync();
        }

        public async Task<List<DropdownViewModel>> GetCircuitsAsync(int? districtId = null)
        {
            var query = _context.Circuits
                .Where(x => !x.IsDeleted)
                .AsQueryable();

            if (districtId.HasValue)
            {
                query = query.Where(x => x.DistrictId == districtId.Value);
            }

            return await query
                .OrderBy(x => x.Name)
                .Select(x => new DropdownViewModel
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToListAsync();
        }

        public async Task<List<DropdownViewModel>> GetSocietiesAsync(int? circuitId = null)
        {
            var query = _context.Societies
                .Where(x => !x.IsDeleted && x.IsActive)
                .AsQueryable();

            if (circuitId.HasValue)
            {
                query = query.Where(x => x.CircuitId == circuitId.Value);
            }

            return await query
                .OrderBy(x => x.Name)
                .Select(x => new DropdownViewModel
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToListAsync();
        }

        public async Task<List<DropdownViewModel>> GetYearsAsync()
        {
            return await _context.StatisticalYears
                .Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.Year)
                .Select(x => new DropdownViewModel
                {
                    Id = x.Id,
                    Name = x.Year.ToString()
                })
                .ToListAsync();
        }

        public async Task<List<StatisticalCategoryViewModel>> GetCategoriesAsync()
        {
            return await _context.StatisticalCategories
                .Where(x => !x.IsDeleted)
                .OrderBy(x => x.DisplayOrder)
                .Select(x => new StatisticalCategoryViewModel
                {
                    Id = x.Id,
                    Code = x.Code,
                    Name = x.Name,
                    CategoryGroup = x.CategoryGroup,
                    IsBoolean = x.IsBoolean,
                    IsSystemGenerated = x.IsSystemGenerated,
                    DisplayOrder = x.DisplayOrder,
                    Description = x.Description
                })
                .ToListAsync();
        }
    }
}
