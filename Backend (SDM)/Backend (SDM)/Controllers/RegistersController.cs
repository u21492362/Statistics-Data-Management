using Backend__SDM_.Models.ViewModels;
using Backend__SDM_.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend__SDM_.Controllers.Api
{
    [Authorize]
    [ApiController]
    [Route("api/registers")]
    public class RegistersController : ControllerBase
    {
        private readonly IRegisterService _registerService;

        public RegistersController(IRegisterService registerService)
        {
            _registerService = registerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? yearId, [FromQuery] int? societyId)
        {
            var result = await _registerService.GetRegistersAsync(yearId, societyId);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _registerService.GetRegisterAsync(id);
            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StatisticalRegisterViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var id = await _registerService.CreateRegisterAsync(model);
                return Ok(new { id });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("{id:int}/submit")]
        public async Task<IActionResult> Submit(int id)
        {
            var success = await _registerService.SubmitRegisterAsync(id);
            if (!success) return NotFound();

            return Ok(new { message = "Register submitted successfully." });
        }

        [HttpPost("{id:int}/finalise")]
        public async Task<IActionResult> Finalise(int id)
        {
            var success = await _registerService.FinaliseRegisterAsync(id);
            if (!success) return NotFound();

            return Ok(new { message = "Register finalised successfully." });
        }

        [HttpGet("{id:int}/capture")]
        public async Task<IActionResult> GetCapture(int id)
        {
            var result = await _registerService.GetRegisterCaptureAsync(id);
            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpPost("{id:int}/entries")]
        public async Task<IActionResult> AddMemberToRegister(int id, [FromBody] AddRegisterMemberRequest model)
        {
            try
            {
                var entryId = await _registerService.AddMemberToRegisterAsync(id, model.MemberId, model.Remarks);
                return Ok(new { entryId });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("entries/{entryId:int}/categories")]
        public async Task<IActionResult> SaveEntryCategories(int entryId, [FromBody] List<RegisterMemberCategoryViewModel> categories)
        {
            var success = await _registerService.SaveEntryCategoriesAsync(entryId, categories);
            if (!success) return NotFound();

            return Ok(new { message = "Entry categories saved successfully." });
        }
    }

    public class AddRegisterMemberRequest
    {
        public int MemberId { get; set; }
        public string? Remarks { get; set; }
    }
}
