using AuctionManagementSystem.Application.Dtos.Settings;
using AuctionManagementSystem.Application.Features.Settings.SystemSettings.Command.CreateSystemSettings;
using AuctionManagementSystem.Application.Features.Settings.SystemSettings.Command.DeleteSystemSettings;
using AuctionManagementSystem.Application.Features.Settings.SystemSettings.Command.UpdateSystemSettings;
using AuctionManagementSystem.Application.Features.Settings.SystemSettings.Query.GetSystemSettingById;
using AuctionManagementSystem.Application.Features.Settings.SystemSettings.Query.GetSystemSettings;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Api.Controllers.Settings
{
    [ApiController]
    [Route("api/auction-settings")]
    public class AuctionSettingsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuctionSettingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
    
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetSystemSettingsQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
       
        public async Task<IActionResult> GetSystemSettingById(int id)
        {
            var result = await _mediator.Send(new GetSystemSettingByIdQuery(id));
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
     
        public async Task<IActionResult> Create([FromBody] SystemSettingsDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _mediator.Send(new CreateSystemSettingsCommand(dto));
            return CreatedAtAction(nameof(GetSystemSettingById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
      
        public async Task<IActionResult> Update(int id, [FromBody] SystemSettingsDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            dto.Id = id;
            var result = await _mediator.Send(new UpdateSystemSettingsCommand(dto));
            return result != null ? Ok(result) : NotFound();
        }

        [HttpDelete("{id}")]
     
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteSystemSettingsCommand(id));
            return result ? NoContent() : NotFound();
        }
    }
}
