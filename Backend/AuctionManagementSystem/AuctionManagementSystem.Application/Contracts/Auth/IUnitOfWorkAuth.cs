using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.User;

namespace AuctionManagementSystem.Application.Contracts.Auth
{
    public interface IUnitOfWorkAuth
    {
        IOtpRepository OtpRepository { get; }

        IUserRepository UserRepository { get; }
        IRoleRepository RoleRepository { get; }
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }

}
