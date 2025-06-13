using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Application.Contracts.AuditTrail;
using AuctionManagementSystem.Application.Contracts.User;
using AuctionManagementSystem.Application.Dtos.AuditTrial;
using AuctionManagementSystem.Application.Exceptions;
using AuctionManagementSystem.Domain.Entities.Asset;
using AuctionManagementSystem.Domain.Entities.Translations;
using AutoMapper;
using MediatR;
using Newtonsoft.Json;

namespace AuctionManagementSystem.Application.Features.Assets.AssetCategory.Command.CreateAssetCategory
{
    public class CreateAssetCategoryHandler : IRequestHandler<CreateAssetCategoryCommand, int>
    {
        private readonly IAssetCategoriesRepository _repository;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        private readonly IAuditTrailService _auditTrailService;
        private readonly ICurrentUserService _currentUser;

        public CreateAssetCategoryHandler(
            IAuditTrailService auditTrailService,
            ICurrentUserService currentUser,
            IAssetCategoriesRepository repository,
            IFileService fileService,
            IMapper mapper)
        {
            _repository = repository;
            _fileService = fileService;
            _mapper = mapper;
            _auditTrailService = auditTrailService;
            _currentUser = currentUser;
        }

        public async Task<int> Handle(CreateAssetCategoryCommand request, CancellationToken cancellationToken)
        {
            var dto = request.AssetCategory;

            var existingCategory = await _repository.GetByNameAsync(dto.CategoryName.Trim());
            if (existingCategory != null)
                throw new BadRequestException("An asset category with the same name already exists.");

            var entity = _mapper.Map<TblAssetCategory>(dto);

            // Save icon
            if (dto.IconFile != null)
                entity.Icon = await _fileService.SaveFileAsync(dto.IconFile, "CategoryIcons");

            // Save document
            if (dto.Document != null)
                entity.DocumentPath = await _fileService.SaveFileAsync(dto.Document, "CategoryDocuments");

            // Save entity
            var created = await _repository.AddWithPaymentMethodsAsync(entity, dto.PaymentMethodIds);
            if (dto.LanguageId.HasValue && dto.LanguageId.Value != 0)
            {
                var translation = new tblAssetCategoryTranslations
                {
                    CategoryId = created.CategoryId,
                    LanguageId = dto.LanguageId.Value,
                    TranslatedCategoryName = dto.TranslatedCategoryName,
                    TranslatedSubcategory = dto.TranslatedSubcategory,
                    TranslatedDetails = dto.TranslatedDetails,
                    CreatedAt = DateTime.UtcNow
                };

                await _repository.AddAssetCategoryTranslationAsync(translation);
            }


            // Prepare DTO for audit trail
            var afterDto = new AuditAssetCategoryDto
            {
                CategoryId = created.CategoryId,
                CategoryName = created.CategoryName,
                Icon = created.Icon,
                DocumentPath = created.DocumentPath,
                IsDeleted = created.IsDeleted,
                PaymentMethodIds = dto.PaymentMethodIds
            };

            // Log audit
            await _auditTrailService.LogChangeAsync(
                userId: _currentUser.UserId,
                username: _currentUser.Username,
                roleName: _currentUser.RoleName,
                modelName: "AssetCategory",
                changeType: "New",
                recordId: created.CategoryId,
                beforeChange: null,
                afterChange: JsonConvert.SerializeObject(afterDto)
            );

            return created.CategoryId;
        }
    }
}
