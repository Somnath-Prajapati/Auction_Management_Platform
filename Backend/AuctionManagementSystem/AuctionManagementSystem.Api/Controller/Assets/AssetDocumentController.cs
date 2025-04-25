using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Application.Features.Assets.AssetDocuments.Command.AddDocument;
using AuctionManagementSystem.Application.Features.Assets.AssetDocuments.Command.DeleteDocument;
using AuctionManagementSystem.Application.Features.Assets.AssetDocuments.Command.UpdateDocument;
using AuctionManagementSystem.Application.Features.Assets.AssetDocuments.Query.GetDocument;
using AuctionManagementSystem.Application.Features.Assets.AssetDocuments.Query.GetDocumentById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuctionManagementSystem.Api.Controller.Assets
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetDocumentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AssetDocumentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromForm] AssetDocumentUploadDto dto)
        {
            var id = await _mediator.Send(new AddAssetDocumentCommand(dto));
            return Ok(new { DocumentId = id });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromForm] AssetDocumentUploadDto dto)
        {
            var result = await _mediator.Send(new UpdateAssetDocumentCommand(id, dto));
            return result ? Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteAssetDocumentCommand(id));
            return result ? Ok() : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var docs = await _mediator.Send(new GetAllAssetDocumentsQuery());
            return Ok(docs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var doc = await _mediator.Send(new GetAssetDocumentByIdQuery(id));
            return doc != null ? Ok(doc) : NotFound();
        }
    }
}
