using Backend__SDM_.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend__SDM_.Controllers.Api
{
    [Authorize]
    [ApiController]
    [Route("api/reports")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly IDashboardService _dashboardService;

        public ReportsController(IReportService reportService, IDashboardService dashboardService)
        {
            _reportService = reportService;
            _dashboardService = dashboardService;
        }

        [HttpGet("society-summary")]
        public async Task<IActionResult> GetSocietySummary([FromQuery] int yearId, [FromQuery] int societyId)
        {
            var result = await _reportService.GetSocietySummaryAsync(yearId, societyId);
            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpGet("circuit-summary")]
        public async Task<IActionResult> GetCircuitSummary([FromQuery] int yearId, [FromQuery] int circuitId)
        {
            var result = await _reportService.GetCircuitSummaryAsync(yearId, circuitId);
            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboard()
        {
            var result = await _dashboardService.GetDashboardAsync();
            return Ok(result);
        }
    }
}
