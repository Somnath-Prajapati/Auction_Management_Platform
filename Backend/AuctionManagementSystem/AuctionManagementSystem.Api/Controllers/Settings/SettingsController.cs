using AuctionManagementSystem.Application.Dtos.Settings;
using AuctionManagementSystem.Application.Features.Settings.SystemSettings.Command.CreateSystemSettings;
using AuctionManagementSystem.Application.Features.Settings.SystemSettings.Command.DeleteSystemSettings;
using AuctionManagementSystem.Application.Features.Settings.SystemSettings.Command.UpdateSystemSettings;
using AuctionManagementSystem.Application.Features.Settings.SystemSettings.Query.GetSystemSettingById;
using AuctionManagementSystem.Application.Features.Settings.SystemSettings.Query.GetSystemSettings;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuctionManagementSystem.Api.Controllers.Settings
{
    [ApiController]
    [Route("api/[controller]")]
    public class SystemSettingsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SystemSettingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/SystemSettings
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetSystemSettingsQuery());
            return Ok(result);
        }

        // GET: api/SystemSettings/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSystemSettingById(int id)
        {
            var query = new GetSystemSettingByIdQuery(id);
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST: api/SystemSettings
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SystemSettingsDto dto)
        {
            var result = await _mediator.Send(new CreateSystemSettingsCommand(dto));
            return CreatedAtAction(nameof(GetSystemSettingById), new { id = result.Id }, result);
        }

        // PUT: api/SystemSettings/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SystemSettingsDto dto)
        {
            dto.Id = id;
            var result = await _mediator.Send(new UpdateSystemSettingsCommand(dto));
            return result != null ? Ok(result) : NotFound();
        }

        // DELETE: api/SystemSettings/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteSystemSettingsCommand(id));
            return result ? NoContent() : NotFound();
        }
    }
}
