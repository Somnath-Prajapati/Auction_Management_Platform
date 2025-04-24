using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Domain.Entities.Settings;

namespace AuctionManagementSystem.Application.Contracts.Settings
{
    public interface IFinanceSettingsRepository
    {
        Task<TblFinanceSetting> AddAsync(TblFinanceSetting setting, CancellationToken cancellationToken);
        Task<TblFinanceSetting?> GetByIdAsync(int id);
        Task UpdateAsync(TblFinanceSetting settings);
        Task DeleteAsync(TblFinanceSetting settings);

        Task<List<TblFinanceSetting>> GetAllAsync();

    }


}
