using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Auth;
using AuctionManagementSystem.Application.Contracts.User;
using AuctionManagementSystem.Persistence.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace AuctionManagementSystem.Identity.Repository
{
    public class UnitOfWorkAuth : IUnitOfWorkAuth
    {
        private readonly AuctionManagementDbContext _context;
        private IDbContextTransaction _transaction;

        public IOtpRepository OtpRepository { get; }
        public IUserRepository UserRepository { get; }
        public IRoleRepository RoleRepository {  get; }

        public UnitOfWorkAuth(AuctionManagementDbContext context, IOtpRepository otpRepository,IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _context = context;
            OtpRepository = otpRepository;
            UserRepository = userRepository;    
            RoleRepository = roleRepository;
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
            await _transaction.CommitAsync();
        }

        public async Task RollbackAsync()
        {
            await _transaction.RollbackAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }

}
