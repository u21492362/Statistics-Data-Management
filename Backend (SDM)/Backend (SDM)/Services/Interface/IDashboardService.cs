using Backend__SDM_.Models.ViewModels;

namespace Backend__SDM_.Services.Interface
{
    public interface IDashboardService
    {
        Task<DashboardViewModel> GetDashboardAsync();
    }
}
