using AuctionManagementSystem.Application.Dtos.Settings;
using AuctionManagementSystem.Application.Features.Settings.DirectSaleSettings.Commands.DeleteDirectSaleSettings;
using AuctionManagementSystem.Application.Features.Settings.DirectSaleSettings.Queries.GetDirectSaleSettingsById;
using AuctionManagementSystem.Application.Features.Settings.DirectSaleSettings.Queries.GetAllDirectSaleSettings;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AuctionManagementSystem.Application.Features.Settings.DirectSaleSettings.Commands.CreateDirectSaleSettings;
using AuctionManagementSystem.Application.Features.Settings.DirectSaleSettings.Commands.UpdateDirectSaleSettings;

namespace AuctionManagementSystem.Api.Controllers.Settings
{
    [ApiController]
    [Route("api/[controller]")]
    public class DirectSaleSettingsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DirectSaleSettingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetDirectSaleSettingByIdQuery(id));
            return Ok(result);
        }


        [HttpGet(Name = "GetAllDirectSaleSettings")]
        public async Task<ActionResult<IEnumerable<DirectSaleSettingsDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllDirectSaleSettingsQuery());
            return Ok(result);
        }

        [HttpPost(Name = "CreateDirectSaleSettings")]
        public async Task<ActionResult<DirectSaleSettingsDto>> Create([FromBody] CreateDirectSaleSettingsCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtRoute("GetDirectSaleSettingsById", new { id = result.Id }, result);
        }

        [HttpPut("{id}", Name = "UpdateDirectSaleSettings")]
        public async Task<ActionResult<DirectSaleSettingsDto>> Update(int id, [FromBody] UpdateDirectSaleSettingsCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID in URL does not match ID in body");

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}", Name = "DeleteDirectSaleSettings")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteDirectSaleSettingCommand(id));
            if (!result)
                return NotFound();

            return NoContent();
        }

    }
}
