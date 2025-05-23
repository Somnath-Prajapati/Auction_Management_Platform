using MediatR;
using AuctionManagementSystem.Application.Contracts.Assets;
using AuctionManagementSystem.Application.Contracts.AuditTrail;
using AuctionManagementSystem.Application.Contracts.User;
using AuctionManagementSystem.Application.Features.Assets.AssetCategory.Command.DeleteAssetCategory;
using Newtonsoft.Json;
using AuctionManagementSystem.Application.Dtos.AuditTrial;

namespace AuctionManagementSystem.Application.Features.AssetCategories.Commands.DeleteAssetCategory
{
    public class DeleteAssetCategoryHandler : IRequestHandler<DeleteAssetCategoryCommand, bool>
    {
        private readonly IAssetCategoriesRepository _repository;
        private readonly IAuditTrailService _auditTrailService;
        private readonly ICurrentUserService _currentUser;

        public DeleteAssetCategoryHandler(
            IAssetCategoriesRepository repository,
            IAuditTrailService auditTrailService,
            ICurrentUserService currentUser)
        {
            _repository = repository;
            _auditTrailService = auditTrailService;
            _currentUser = currentUser;
        }

        public async Task<bool> Handle(DeleteAssetCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetByIdAsync(request.CategoryId);

            if (category != null && !category.IsDeleted)
            {
                // Serialize DTO (not entity) to avoid self-reference loop
                var auditBefore = new AuditAssetCategoryDto
                {
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName,
                    Icon = category.Icon,
                    DocumentPath = category.DocumentPath,
                    IsDeleted = category.IsDeleted,
                    PaymentMethodIds = category.TblAssetCategoryPaymentMethods?
        .Select(pm => pm.PaymentMethodId).ToList() ?? new List<int>()
                };

                var before = JsonConvert.SerializeObject(auditBefore); // This line should NOT fail if DTO is clean.


                category.IsDeleted = true;
                _repository.Update(category);
                await _repository.SaveAsync();

                var auditAfter = new AuditAssetCategoryDto
                {
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName,
                    Icon = category.Icon,
                    DocumentPath = category.DocumentPath,
                    IsDeleted = category.IsDeleted,
                    PaymentMethodIds = category.TblAssetCategoryPaymentMethods?
        .Select(pm => pm.PaymentMethodId).ToList() ?? new List<int>()
                };

                var after = JsonConvert.SerializeObject(auditAfter);


                await _auditTrailService.LogChangeAsync(
                    userId: _currentUser.UserId,
                    username: _currentUser.Username,
                    roleName: _currentUser.RoleName,
                    modelName: "AssetCategory",
                    changeType: "Delete",
                    recordId: category.CategoryId,
                    beforeChange: before,
                    afterChange: after
                );

                return true;
            }

            return false;
        }

    }
}
