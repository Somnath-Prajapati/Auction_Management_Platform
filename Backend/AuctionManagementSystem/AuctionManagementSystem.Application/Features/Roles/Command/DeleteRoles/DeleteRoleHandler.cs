using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.User;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Roles.Command.DeleteRoles
{
    public class DeleteRoleHandler : IRequestHandler<DeleteRoleCommand, bool>
    {
        private readonly IRoleRepository _roleRepository;

        public DeleteRoleHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<bool> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetRoleByIdAsync(request.RoleId);

            if (role == null || role.IsDeleted)
                return false;  // not found or already deleted

            // Soft delete: set IsDeleted flag
            role.IsDeleted = true;

            await _roleRepository.SaveChangesAsync();

            return true;
        }
    }
}
