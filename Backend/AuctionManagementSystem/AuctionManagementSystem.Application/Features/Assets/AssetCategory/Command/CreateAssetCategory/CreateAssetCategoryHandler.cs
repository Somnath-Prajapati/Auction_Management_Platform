using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Application.Contracts.User; // For IFileService
using AuctionManagementSystem.Application.Exceptions;
using AuctionManagementSystem.Domain.Entities.Asset;
using AutoMapper;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Assets.AssetCategory.Command.CreateAssetCategory
{
    public class CreateAssetCategoryHandler : IRequestHandler<CreateAssetCategoryCommand, int>
    {

        private readonly IAssetCategoriesRepository _repository;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public CreateAssetCategoryHandler(IAssetCategoriesRepository repository, IFileService fileService, IMapper mapper)
        {
            _repository = repository;
            _fileService = fileService;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateAssetCategoryCommand request, CancellationToken cancellationToken)
        {
            var dto = request.AssetCategory;

            var existingCategory = await _repository.GetByNameAsync(dto.CategoryName.Trim());
            if (existingCategory != null)
            {
                throw new BadRequestException("An asset category with the same name already exists.");
            }

            var entity = _mapper.Map<TblAssetCategory>(dto);

            // Correct file check and assignment
            if (dto.IconFile != null)
            {
                entity.Icon = await _fileService.SaveFileAsync(dto.IconFile, "CategoryIcons");
            }

            if (dto.Document != null)
            {
                entity.DocumentPath = await _fileService.SaveFileAsync(dto.Document, "CategoryDocuments");
            }

            var created = await _repository.AddAsync(entity);
            return created.CategoryId;
        }
    }
}
    
