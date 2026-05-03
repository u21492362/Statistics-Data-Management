using Backend__SDM_.Models.ViewModels;
using Backend__SDM_.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend__SDM_.Controllers.Api
{
    [Authorize]
    [ApiController]
    [Route("api/members")]
    public class MembersController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public MembersController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? societyId, [FromQuery] string? search)
        {
            var result = await _memberService.GetAllAsync(societyId, search);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _memberService.GetByIdAsync(id);
            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MemberViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var id = await _memberService.CreateAsync(model);
            return Ok(new { id });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] MemberViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var success = await _memberService.UpdateAsync(id, model);
            if (!success) return NotFound();

            return Ok(new { message = "Member updated successfully." });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _memberService.DeleteAsync(id);
            if (!success) return NotFound();

            return Ok(new { message = "Member deleted successfully." });
        }
    }
}
