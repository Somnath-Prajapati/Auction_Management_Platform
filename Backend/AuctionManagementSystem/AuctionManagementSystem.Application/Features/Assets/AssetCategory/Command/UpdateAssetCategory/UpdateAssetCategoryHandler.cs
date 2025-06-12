using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Application.Contracts.AuditTrail;
using AuctionManagementSystem.Application.Contracts.User;
using AuctionManagementSystem.Application.Dtos.AuditTrial;
using AuctionManagementSystem.Application.Features.Assets.AssetCategory.Command.UpdateAssetCategory;
using AuctionManagementSystem.Domain.Entities.Translations;
using AutoMapper;
using MediatR;
using Newtonsoft.Json;

namespace AuctionManagementSystem.Application.Features.AssetCategories.Commands.UpdateAssetCategory
{
    public class UpdateAssetCategoryHandler : IRequestHandler<UpdateAssetCategoryCommand, bool>
    {
        private readonly IAssetCategoriesRepository _repository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IAuditTrailService _auditTrailService;
        private readonly ICurrentUserService _currentUser;

        public UpdateAssetCategoryHandler(
            IAssetCategoriesRepository repository,
            IMapper mapper,
            IFileService fileService,
            IAuditTrailService auditTrailService,
            ICurrentUserService currentUser)
        {
            _repository = repository;
            _mapper = mapper;
            _fileService = fileService;
            _auditTrailService = auditTrailService;
            _currentUser = currentUser;
        }

        public async Task<bool> Handle(UpdateAssetCategoryCommand request, CancellationToken cancellationToken)
        {
            var existingCategory = await _repository.GetByIdAsync(request.CategoryId);
            if (existingCategory == null || existingCategory.IsDeleted)
                return false;

            // Snapshot before update
            var beforeDto = new AuditAssetCategoryDto
            {
                CategoryId = existingCategory.CategoryId,
                CategoryName = existingCategory.CategoryName,
                Icon = existingCategory.Icon,
                DocumentPath = existingCategory.DocumentPath,
                IsDeleted = existingCategory.IsDeleted,
                PaymentMethodIds = existingCategory.TblAssetCategoryPaymentMethods
                    .Select(p => p.PaymentMethodId).ToList()
            };

            var existingIcon = existingCategory.Icon;
            _mapper.Map(request.UpdatedCategory, existingCategory);

            // Icon handling
            if (request.UpdatedCategory.IconFile != null)
            {
                if (!string.IsNullOrEmpty(existingIcon))
                    await _fileService.DeleteFileAsync(existingIcon);

                existingCategory.Icon = await _fileService.SaveFileAsync(request.UpdatedCategory.IconFile, "CategoryIcons");
            }
            else
            {
                existingCategory.Icon = existingIcon;
            }

            // Document handling
            if (request.UpdatedCategory.Document != null)
            {
                existingCategory.DocumentPath = await _fileService.SaveFileAsync(request.UpdatedCategory.Document, "CategoryDocuments");
            }

            existingCategory.UpdatedDate = DateTime.UtcNow;

            await _repository.UpdateWithPaymentMethodsAsync(existingCategory, request.UpdatedCategory.PaymentMethodIds);

            if (request.UpdatedCategory.LanguageId.HasValue && request.UpdatedCategory.LanguageId.Value != 0)
            {
                var langId = request.UpdatedCategory.LanguageId.Value;

                var existingTranslation = await _repository.GetTranslationByLanguageIdAsync(existingCategory.CategoryId, langId);

                if (existingTranslation != null)
                {
                    // Update existing translation
                    existingTranslation.TranslatedCategoryName = request.UpdatedCategory.TranslatedCategoryName;
                    existingTranslation.TranslatedSubcategory = request.UpdatedCategory.TranslatedSubcategory;
                    existingTranslation.TranslatedDetails = request.UpdatedCategory.TranslatedDetails;
                }
                else
                {
                    // Add new translation
                    var newTranslation = new tblAssetCategoryTranslations
                    {
                        CategoryId = existingCategory.CategoryId,
                        LanguageId = langId,
                        TranslatedCategoryName = request.UpdatedCategory.TranslatedCategoryName,
                        TranslatedSubcategory = request.UpdatedCategory.TranslatedSubcategory,
                        TranslatedDetails = request.UpdatedCategory.TranslatedDetails,
                        CreatedAt = DateTime.UtcNow
                    };

                    await _repository.AddAssetCategoryTranslationAsync(newTranslation);
                }

                await _repository.SaveAsync();
            }

            // Snapshot after update
            var afterDto = new AuditAssetCategoryDto
            {
                CategoryId = existingCategory.CategoryId,
                CategoryName = existingCategory.CategoryName,
                Icon = existingCategory.Icon,
                DocumentPath = existingCategory.DocumentPath,
                IsDeleted = existingCategory.IsDeleted,
                PaymentMethodIds = request.UpdatedCategory.PaymentMethodIds
            };

            // Audit log
            await _auditTrailService.LogChangeAsync(
                userId: _currentUser.UserId,
                username: _currentUser.Username,
                roleName: _currentUser.RoleName,
                modelName: "AssetCategory",
                changeType: "Update",
                recordId: existingCategory.CategoryId,
                beforeChange: JsonConvert.SerializeObject(beforeDto),
                afterChange: JsonConvert.SerializeObject(afterDto)
            );

            return true;
        }
    }
}
