using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Assets;

namespace AuctionManagementSystem.Persistence.Repositories.Assets
{
    public class AssetExpirationService : IAssetExpirationService
    {
        private readonly IAssetsRepository _assetsRepository;

        public AssetExpirationService(IAssetsRepository assetsRepository)
        {
            _assetsRepository = assetsRepository;
        }

        public async Task DeactivateExpiredAssetsAsync()
        {
            await _assetsRepository.DeactivateExpiredAssetsBasedOnDeadlineAsync();
        }
    }

}
