using Backend__SDM_.Models.ViewModels;

namespace Backend__SDM_.Services.Interface
{
    public interface IReportService
    {
        Task<SocietySummaryViewModel?> GetSocietySummaryAsync(int yearId, int societyId);
        Task<CircuitSummaryViewModel?> GetCircuitSummaryAsync(int yearId, int circuitId);
    }
}
