using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Application.Features.Assets.Asset.Command;
using AuctionManagementSystem.Application.Features.Assets.Asset.Command.DeleteAsset;
using AuctionManagementSystem.Application.Features.Assets.Asset.Command.UpdateAsset;
using AuctionManagementSystem.Application.Features.Assets.Asset.Query.GetAssetById;
using AuctionManagementSystem.Application.Features.Assets.Asset.Query.GetDirectSaleAssets;
using AuctionManagementSystem.Application.Features.Assets.Asset.Query.GetSellers;
using AuctionManagementSystem.Application.Features.Assets.Asset.Query.SearchAsset;
using AuctionManagementSystem.Application.Features.Assets.AssetAuction.Command.AddAssetAuction;
using AuctionManagementSystem.Application.Features.Assets.AssetDetails.Command;
using AuctionManagementSystem.Application.Features.Assets.AssetDocuments.Command.AddDocument;
using AuctionManagementSystem.Application.Features.Assets.AssetGallery.Command.AddAssetGallery;
using AuctionManagementSystem.Application.Features.Assets.Query.GetAssets;
using AuctionManagementSystem.Domain.Entities.Asset;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AuctionManagementSystem.Api.Controller.Assets
{
    //[EnableCors("AllowAll")]
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AssetsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<GetAssetsFormDto>>> GetAllAssets()
        {
            var assets = await _mediator.Send(new GetAssetsQuery());
            return Ok(assets);
        }

        //[HttpGet("GetForView/{id}")]
        //public async Task<ActionResult<IEnumerable<GetAssetsFormDto>>> GetAllAssetsForViewById(int id)
        //{
        //    var assets = await _mediator.Send(new GetAssetsByIdForViewQuery(id));
        //    return Ok(assets);
        //}

        [HttpGet("getSellers")]
        public async Task<ActionResult<IEnumerable<GetAssetsFormDto>>> GetSellers()
        {
            var sellers = await _mediator.Send(new GetSellersQuery());
            return Ok(sellers);
        }


        [HttpGet("directsaleasset")]
        public async Task<IActionResult> GetDirectSaleAssets([FromQuery] int categoryId)
        {
            if (categoryId <= 0)
                return BadRequest(new { Message = "Invalid category ID." });

            var result = await _mediator.Send(new GetDirectSaleAssetsByCategoryQuery { CategoryId = categoryId });

            if (result == null || result.Count == 0)
                return NotFound(new { Message = "No direct sale assets found for this category." });

            return Ok(result);
        }


        [HttpGet("auctionasset")]
        public async Task<IActionResult> GetAuctionAssets([FromQuery] int categoryId)
        {
            if (categoryId <= 0)
                return BadRequest(new { Message = "Invalid category ID." });

            var result = await _mediator.Send(new GetAuctionAssetsByCategoryQuery { CategoryId = categoryId });

            if (result == null || result.Count == 0)
                return NotFound(new { Message = "No auction assets found for this category." });

            return Ok(result);
        }




        //[Route("add")]
        //[HttpPost]
        //public async Task<ActionResult<GetAssetsDto>> CreateAsset(CreateAssetsDto createAsset)
        //{
        //    var result = await _mediator.Send(new AddAssetCommand(createAsset));

          

        //    return Ok(result);
        //}


        //[Route("GetById")]
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<GetAssetsFormDto>>> GetAsset(int id, [FromQuery] string lang)
        {
            var assets = await _mediator.Send(new GetAssetByIdQuery(id,lang));
            return Ok(assets);
        }



        [HttpPut("{id:int}")]
        public async Task<ActionResult<UpdateAssetDto>> UpdateAsset(int id, [FromBody] UpdateAssetDto assetsDto)
        {
            if (id != assetsDto.AssetId)
                return BadRequest("ID in URL does not match ID in body");
            await _mediator.Send(new UpdateAssetCommand(id, assetsDto));
            return Ok();

        }

        //[Route("Search Asset")]
        [HttpPost("Name:Search Asset")]
        public async Task<ActionResult<UpdateAssetDto>> SearchAsset(string name)
        {
            var assets = await _mediator.Send(new SearchAssetQuery(name));
            return Ok(assets);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UpdateAssetDto>> DeleteAsset(int id)
        {
            await _mediator.Send(new DeleteAssetCommand(id));
            return Ok();
        }


        [HttpPost("CreateWithGallery")]
        public async Task<ActionResult<AssetWithGalleryResponseDto>> CreateAssetWithGallery
            ([FromForm] CreateAssetsDto dto)
        {
            try
            {

                #region
                List<AssetDetailDto> details;
                try
                {
                    details = JsonConvert.DeserializeObject<List<AssetDetailDto>>(dto.DetailsJson ?? "[]");
                    if (details == null) details = new List<AssetDetailDto>();
                }
                catch
                {
                    return BadRequest("Invalid details format");
                }
                #endregion
                var assetResult = await _mediator.Send(new AddUnifiedAssetCommand(dto));

                if (dto.AuctionIds != null && dto.AuctionIds.Any())
                {
                    await _mediator.Send(new AssignAssetToAuctionCommand(assetResult, dto.AuctionIds));
                }

                if (details != null && details.Any())
                {
                    foreach (var detail in details)
                    {
                        var assetDetail = new TblAssetDetail
                        {
                            AssetId = assetResult,
                            AttributeName = detail.AttributeName,
                            AttributeValue = detail.AttributeValue
                        };

                        await _mediator.Send(new AddAssetDetailCommand(assetDetail));
                    }
                }





                var galleryResults = new List<GalleryResultDto>();
                if (dto.GalleryFiles != null && dto.GalleryFiles.Count > 0)
                {
                    foreach (var file in dto.GalleryFiles)
                    {
                        var galleryDto = new AssetsGalleryDto
                        {
                            AssetId = assetResult,
                            File = file,
                            MediaType = "image",
                            SortOrder = 0
                        };

                        var galleryId = await _mediator.Send(new AddAssetGalleryCommand(galleryDto));
                        //galleryResults.Add(new GalleryResultDto { GalleryId = galleryId, FileName = file.FileName });
                    }
                }


                var documentResults = new List<DocumentResultDto>();
                if (dto.DocumentFiles != null && dto.DocumentFiles.Any())
                {
                    foreach (var file in dto.DocumentFiles)
                    {
                        var documentDto = new AssetDocumentUploadDto
                        {
                            AssetId = assetResult,
                            File = file,
                            DocumentType = "pdf"
                        };

                        var documentId = await _mediator.Send(new AddAssetDocumentCommand(documentDto));
                        //documentResults.Add(new DocumentResultDto { DocumentId = documentId, FileName = file.FileName });
                    }
                }



                return Ok(new AssetWithGalleryResponseDto
                {
                    AssetId = assetResult,
                    GalleryResults = galleryResults,
                    Message = "Asset created successfully with gallery images"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Message = "Error creating asset with gallery",
                    Error = ex.Message,
                    InnerError = ex.InnerException?.Message
                });
            }
        }


        // Response DTOs
        public class AssetWithGalleryResponseDto
        {
            public int AssetId { get; set; }
            public List<GalleryResultDto> GalleryResults { get; set; }
            public string Message { get; set; }
        }

        public class GalleryResultDto
        {
            public int GalleryId { get; set; }
            public string FileName { get; set; }
        }

        public class DocumentResultDto
        {
            public int DocumentId { get; set; }
            public string FileName { get; set; }
        }


        [HttpPut("update-asset-all")]
        public async Task<IActionResult> UpdateAsset([FromForm] UpdateAssetAllDto dto)
        {
            var result = await _mediator.Send(new UpdateAssetAllCommand(dto));

            return Ok(result);
        }

    }
}
