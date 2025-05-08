using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Auth;
using AuctionManagementSystem.Application.Contracts.User;
using AuctionManagementSystem.Persistence.Context;

namespace AuctionManagementSystem.Identity.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AuctionManagementDbContext _context;

        public IOtpRepository OtpRepository { get; }
        public IUserRepository UserRepository { get; }
        public IRoleRepository RoleRepository {  get; }

        public UnitOfWork(AuctionManagementDbContext context, IOtpRepository otpRepository,IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _context = context;
            OtpRepository = otpRepository;
            UserRepository = userRepository;
            RoleRepository = roleRepository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }

}
