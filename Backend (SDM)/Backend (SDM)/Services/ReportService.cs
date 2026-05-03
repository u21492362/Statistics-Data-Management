using Backend__SDM_.Models.Data;
using Backend__SDM_.Models.ViewModels;
using Backend__SDM_.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Backend__SDM_.Services
{
    public class ReportService : IReportService
    {
        private readonly ApplicationDbContext _context;

        public ReportService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<SocietySummaryViewModel?> GetSocietySummaryAsync(int yearId, int societyId)
        {
            var register = await _context.StatisticalRegisters
                .Include(x => x.StatisticalYear)
                .Include(x => x.District)
                .Include(x => x.Circuit)
                .Include(x => x.Society)
                .FirstOrDefaultAsync(x =>
                    !x.IsDeleted &&
                    x.StatisticalYearId == yearId &&
                    x.SocietyId == societyId);

            if (register == null) return null;

            var items = await _context.RegisterMemberCategories
                .Where(x =>
                    !x.IsDeleted &&
                    x.RegisterMemberEntry != null &&
                    x.RegisterMemberEntry.StatisticalRegisterId == register.Id &&
                    x.ValueBool)
                .GroupBy(x => new
                {
                    x.StatisticalCategoryId,
                    x.StatisticalCategory!.Code,
                    x.StatisticalCategory.Name
                })
                .Select(g => new SocietySummaryItemViewModel
                {
                    CategoryCode = g.Key.Code,
                    CategoryName = g.Key.Name,
                    Total = g.Count()
                })
                .OrderBy(x => x.CategoryName)
                .ToListAsync();

            var totalMembers = await _context.RegisterMemberEntries
                .CountAsync(x => !x.IsDeleted && x.StatisticalRegisterId == register.Id);

            return new SocietySummaryViewModel
            {
                StatisticalYearId = register.StatisticalYearId,
                Year = register.StatisticalYear?.Year ?? 0,
                DistrictId = register.DistrictId,
                DistrictName = register.District?.Name ?? string.Empty,
                CircuitId = register.CircuitId,
                CircuitName = register.Circuit?.Name ?? string.Empty,
                SocietyId = register.SocietyId,
                SocietyName = register.Society?.Name ?? string.Empty,
                TotalRegisteredMembers = totalMembers,
                Items = items
            };
        }

        public async Task<CircuitSummaryViewModel?> GetCircuitSummaryAsync(int yearId, int circuitId)
        {
            var year = await _context.StatisticalYears.FirstOrDefaultAsync(x => x.Id == yearId && !x.IsDeleted);
            var circuit = await _context.Circuits
                .Include(x => x.District)
                .Include(x => x.Societies)
                .FirstOrDefaultAsync(x => x.Id == circuitId && !x.IsDeleted);

            if (year == null || circuit == null) return null;

            var categorySummary = await _context.RegisterMemberCategories
                .Where(x =>
                    !x.IsDeleted &&
                    x.ValueBool &&
                    x.RegisterMemberEntry != null &&
                    x.RegisterMemberEntry.StatisticalRegister != null &&
                    x.RegisterMemberEntry.StatisticalRegister.StatisticalYearId == yearId &&
                    x.RegisterMemberEntry.StatisticalRegister.CircuitId == circuitId)
                .Select(x => new
                {
                    CategoryId = x.StatisticalCategoryId,
                    CategoryCode = x.StatisticalCategory!.Code,
                    CategoryName = x.StatisticalCategory.Name,
                    SocietyId = x.RegisterMemberEntry!.StatisticalRegister!.SocietyId,
                    SocietyName = x.RegisterMemberEntry.StatisticalRegister.Society!.Name
                })
                .ToListAsync();

            var grouped = categorySummary
                .GroupBy(x => new { x.CategoryId, x.CategoryCode, x.CategoryName })
                .Select(g => new CircuitSummaryItemViewModel
                {
                    CategoryCode = g.Key.CategoryCode,
                    CategoryName = g.Key.CategoryName,
                    CircuitTotal = g.Count(),
                    SocietyBreakdown = g.GroupBy(s => new { s.SocietyId, s.SocietyName })
                        .Select(sg => new CircuitSocietyBreakdownViewModel
                        {
                            SocietyId = sg.Key.SocietyId,
                            SocietyName = sg.Key.SocietyName,
                            Total = sg.Count()
                        })
                        .OrderBy(x => x.SocietyName)
                        .ToList()
                })
                .OrderBy(x => x.CategoryName)
                .ToList();

            var totalMembers = await _context.RegisterMemberEntries
                .CountAsync(x =>
                    !x.IsDeleted &&
                    x.StatisticalRegister != null &&
                    x.StatisticalRegister.StatisticalYearId == yearId &&
                    x.StatisticalRegister.CircuitId == circuitId);

            return new CircuitSummaryViewModel
            {
                StatisticalYearId = year.Id,
                Year = year.Year,
                DistrictId = circuit.DistrictId,
                DistrictName = circuit.District?.Name ?? string.Empty,
                CircuitId = circuit.Id,
                CircuitName = circuit.Name,
                TotalSocieties = circuit.Societies.Count(x => !x.IsDeleted && x.IsActive),
                TotalRegisteredMembers = totalMembers,
                Items = grouped
            };
        }
    }
}
