using AuctionManagementSystem.Application.Dtos.Settings;
using AuctionManagementSystem.Application.Features.Settings.StaticPagesSettings.Command.CreateStaticPagesSettings;
using AuctionManagementSystem.Application.Features.Settings.StaticPagesSettings.Command.DeleteStaticPagesSettings;
using AuctionManagementSystem.Application.Features.Settings.StaticPagesSettings.Command.UpdateStaticPagesSettings;
using AuctionManagementSystem.Application.Features.Settings.StaticPagesSettings.Query.GetAllStaticPagesSettings;
using AuctionManagementSystem.Application.Features.Settings.StaticPagesSettings.Query.GetStaticPagesSettingsById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AuctionManagementSystem.API.Controllers.Settings
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaticPagesSettingsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StaticPagesSettingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStaticPagesSettingsCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result); // Or use CreatedAtAction if ID is available
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllStaticPagesSettingsQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetStaticPagesSettingsByIdQuery(id));
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [HttpPut("Update")]
       
        public async Task<IActionResult> Update( [FromBody] UpdateStaticPagesSettingsCommand command)
        {
            //if (command.Id != id)
            //    return BadRequest("ID mismatch");

            var result = await _mediator.Send(command);
            return Ok(result);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteStaticPagesSettingsCommand(id));
            return Ok(result);
        }
    }
    //public class GetStaticPagesSettingsByIdQuery : IRequest<StaticPagesSettingsDto>
    //{
    //    public int Id { get; }

    //    public GetStaticPagesSettingsByIdQuery(int id)
    //    {
    //        Id = id;
    //    }
    //}
}
