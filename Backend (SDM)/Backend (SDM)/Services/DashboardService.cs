using Backend__SDM_.Entities;
using Backend__SDM_.Models.Data;
using Backend__SDM_.Models.Enums;
using Backend__SDM_.Models.ViewModels;
using Backend__SDM_.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Backend__SDM_.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ApplicationDbContext _context;

        public DashboardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardViewModel> GetDashboardAsync()
        {
            var topCategories = await _context.RegisterMemberCategories
                .Where(x => !x.IsDeleted && x.ValueBool)
                .GroupBy(x => new
                {
                    x.StatisticalCategory!.Code,
                    x.StatisticalCategory.Name
                })
                .Select(g => new SocietySummaryItemViewModel
                {
                    CategoryCode = g.Key.Code,
                    CategoryName = g.Key.Name,
                    Total = g.Count()
                })
                .OrderByDescending(x => x.Total)
                .Take(10)
                .ToListAsync();

            return new DashboardViewModel
            {
                TotalMembers = await _context.Members.CountAsync(x => !x.IsDeleted),
                TotalSocieties = await _context.Societies.CountAsync(x => !x.IsDeleted && x.IsActive),
                TotalRegisters = await _context.StatisticalRegisters.CountAsync(x => !x.IsDeleted),
                DraftRegisters = await _context.StatisticalRegisters.CountAsync(x => !x.IsDeleted && x.Status == RegisterStatus.Draft),
                SubmittedRegisters = await _context.StatisticalRegisters.CountAsync(x => !x.IsDeleted && x.Status == RegisterStatus.Submitted),
                ApprovedRegisters = await _context.StatisticalRegisters.CountAsync(x => !x.IsDeleted && x.Status == RegisterStatus.Approved),
                FinalisedRegisters = await _context.StatisticalRegisters.CountAsync(x => !x.IsDeleted && x.Status == RegisterStatus.Finalised),
                TopCategories = topCategories
            };
        }
    }
}
