using Backend__SDM_.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend__SDM_.Controllers.Api
{
    [Authorize]
    [ApiController]
    [Route("api/lookups")]
    public class LookupController : ControllerBase
    {
        private readonly ILookupService _lookupService;

        public LookupController(ILookupService lookupService)
        {
            _lookupService = lookupService;
        }

        [HttpGet("districts")]
        public async Task<IActionResult> GetDistricts()
        {
            var result = await _lookupService.GetDistrictsAsync();
            return Ok(result);
        }

        [HttpGet("circuits")]
        public async Task<IActionResult> GetCircuits([FromQuery] int? districtId)
        {
            var result = await _lookupService.GetCircuitsAsync(districtId);
            return Ok(result);
        }

        [HttpGet("societies")]
        public async Task<IActionResult> GetSocieties([FromQuery] int? circuitId)
        {
            var result = await _lookupService.GetSocietiesAsync(circuitId);
            return Ok(result);
        }

        [HttpGet("years")]
        public async Task<IActionResult> GetYears()
        {
            var result = await _lookupService.GetYearsAsync();
            return Ok(result);
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var result = await _lookupService.GetCategoriesAsync();
            return Ok(result);
        }
    }
}
