
using AuctionManagementSystem.Application.Features.Settings.FooterLinksSettings.Command.CreateFooterLinksSettings;
using AuctionManagementSystem.Application.Features.Settings.FooterLinksSettings.Command.DeleteFooterLinksSettings;
using AuctionManagementSystem.Application.Features.Settings.FooterLinksSettings.Command.UpdateFooterLinksSettings;
using AuctionManagementSystem.Application.Features.Settings.FooterLinksSettings.Query.GetFooterLinksSettingById;
using AuctionManagementSystem.Application.Features.Settings.FooterLinksSettings.Query.GetFooterLinksSettings;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuctionManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FooterLinksSettingsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FooterLinksSettingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/FooterLinksSettings/Get
        [HttpGet("Get")]
        public async Task<IActionResult> GetFooterLinksSetting()
        {
            var result = await _mediator.Send(new GetFooterLinksSettingsQuery());
            return Ok(result);
        }

        // GET: api/FooterLinksSettings/GetById/5
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetFooterLinksSettingById(int id)
        {
            var result = await _mediator.Send(new GetFooterLinksSettingByIdQuery(id));
            if (result == null) return NotFound();
            return Ok(result);
        }

        // POST: api/FooterLinksSettings/Create
        [HttpPost("Create")]
        public async Task<IActionResult> CreateFooterLinksSetting([FromBody] CreateFooterLinksSettingsCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetFooterLinksSettingById), new { id = result.Id }, result);
        }

        // PUT: api/FooterLinksSettings/Update/5
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateFooterLinksSetting(int id, [FromBody] UpdateFooterLinksSettingsCommand command)
        {
            if (id != command.FooterLinksSettings.Id)
                return BadRequest("ID mismatch");

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // DELETE: api/FooterLinksSettings/Delete/5
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteFooterLinksSetting(int id)
        {
            var result = await _mediator.Send(new DeleteFooterLinksSettingCommand(id));
            if (!result) return NotFound();
            return Ok("Deleted successfully");
        }
    }
}
