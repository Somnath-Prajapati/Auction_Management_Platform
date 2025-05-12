using AuctionManagementSystem.Application.Features.Settings.FinanceSettings.Command.DeleteFinanceSettings;
using AuctionManagementSystem.Application.Features.Settings.FinanceSettings.Command.CreateFinanceSettings;
using AuctionManagementSystem.Application.Features.Settings.FinanceSettings.Command.UpdateFinanceSettings;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AuctionManagementSystem.Application.Features.Settings.FinanceSettings.Query.GetFinanceSettingsById;
using AuctionManagementSystem.Application.Features.Settings.FinanceSettings.Query.GetAllFinanceSettings;
using System.ComponentModel.DataAnnotations;

namespace AuctionManagementSystem.API.Controllers.Settings
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinanceSettingsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FinanceSettingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/FinanceSettings
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllFinanceSettings()
        {
            var result = await _mediator.Send(new GetAllFinanceSettingsQuery());
            return Ok(result);
        }

        // GET: api/FinanceSettings/{id}
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetFinanceSettingById(int id)
        {
            var result = await _mediator.Send(new GetFinanceSettingsByIdQuery(id));
            if (result == null) return NotFound();
            return Ok(result);
        }

        // POST: api/FinanceSettings
        [HttpPost("Create")]
        public async Task<IActionResult> CreateFinanceSetting([FromBody] CreateFinanceSettingsCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetFinanceSettingById), new { id = result.Id }, result);
        }

        // PUT: api/FinanceSettings/{id}
        [HttpPut("update")]
        public async Task<IActionResult> UpdateFinanceSetting([FromBody] UpdateFinanceSettingsCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
            //try
            //{
            //    var result = await _mediator.Send(command);
            //    return Ok(result);
            //}
            //catch (ValidationException ex)
            //{
            //    return BadRequest(new { errors = ex.Errors });
            //}
            //catch (KeyNotFoundException ex)
            //{
            //    return NotFound(new { message = ex.Message });
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, new { message = ex.Message });
            //}
        }


        // DELETE: api/FinanceSettings/{id}
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteFinanceSetting(int id)
        {
            var result = await _mediator.Send(new DeleteFinanceSettingsCommand(id));
            if (!result) return NotFound();
            return Ok("Deleted successfully");
        }
    }
}
