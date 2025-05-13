using AuctionManagementSystem.Application.Features.Settings.FooterLinksSettings.Command.CreateFooterLinksSettings;
using AuctionManagementSystem.Application.Features.Settings.FooterLinksSettings.Command.DeleteFooterLinksSettings;
using AuctionManagementSystem.Application.Features.Settings.FooterLinksSettings.Command.UpdateFooterLinksSettings;
using AuctionManagementSystem.Application.Features.Settings.FooterLinksSettings.Query.GetFooterLinksSettingById;
using AuctionManagementSystem.Application.Features.Settings.FooterLinksSettings.Query.GetFooterLinksSettings;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuctionManagementSystem.API.Controllers.Settings
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

        // Existing code...

        [HttpGet]
        public async Task<IActionResult> GetFooterLinksSettings()
        {
            var result = await _mediator.Send(new GetFooterLinksSettingsQuery());
            if (result == null ) // Ensure 'result' is a collection that supports 'Any()'
                return NotFound("No footer links settings found.");
            return Ok(result);
        }

        // GET: api/footer-links/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFooterLinksSettingById(int id)
        {
            var result = await _mediator.Send(new GetFooterLinksSettingByIdQuery(id));
            if (result == null)
                return NotFound($"Footer link setting with id {id} not found.");
            return Ok(result);
        }

        // POST: api/footer-links
        [HttpPost]
        public async Task<IActionResult> CreateFooterLinksSetting([FromBody] CreateFooterLinksSettingsCommand command)
        {
            if (command == null)
                return BadRequest("Invalid request data.");

            var result = await _mediator.Send(command);
            if (result == null)
                return BadRequest("Footer link setting could not be created.");

            return CreatedAtAction(nameof(GetFooterLinksSettingById), new { id = result.Id }, result);
        }

        // PUT: api/footer-links/{id}
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateFooterLinksSetting( [FromBody] UpdateFooterLinksSettingsCommand command)
        {
            //if (command == null || id != command.FooterLinksSettings.Id)
            //    return BadRequest("ID mismatch or invalid request data.");

            var result = await _mediator.Send(command);
            //if (result == null)
            //    return NotFound($"Footer link setting with id {id} not found.");

            return Ok(result);
        }

        // DELETE: api/footer-links/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFooterLinksSetting(int id)
        {
            var result = await _mediator.Send(new DeleteFooterLinksSettingCommand(id));
            if (!result)
                return NotFound($"Footer link setting with id {id} not found or could not be deleted.");

            return Ok("Deleted successfully.");
        }
    }
}
