using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.User;
using AuctionManagementSystem.Domain.Entities.Roles;
using AutoMapper;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Roles.Command.UpdateRoles
{
    public class UpdateRoleWithPermissionsHandler : IRequestHandler<UpdateRoleWithPermissionsCommand, bool>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public UpdateRoleWithPermissionsHandler(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateRoleWithPermissionsCommand request, CancellationToken cancellationToken)
        {
            var roleDto = request.RoleDto;

            // Fetch existing role including permissions from the repo
            var existingRole = await _roleRepository.GetRoleByIdWithPermissionsAsync(roleDto.RoleId);
            if (existingRole == null) return false; // role not found

            // Map updated values from DTO to existing Role entity (ignoring RoleId)
            _mapper.Map(roleDto, existingRole);

            // Assuming one permissions matrix per role
            var existingPermissions = existingRole.TblRolePermissionsMatrices.FirstOrDefault();
            if (existingPermissions == null)
            {
                // If permissions don't exist, create new
                existingPermissions = _mapper.Map<TblRolePermissionsMatrix>(roleDto);
                existingPermissions.RoleId = existingRole.RoleId;
                await _roleRepository.AddRolePermissionsAsync(existingPermissions);
            }
            else
            {
                // Update existing permissions with new DTO values
                _mapper.Map(roleDto, existingPermissions);
            }

            // Save all changes via DbContext inside repo
            // You might want to add a SaveChangesAsync method on repo to batch update

            // For now, let's assume repository saves changes immediately when updating

            await _roleRepository.SaveChangesAsync();

            return true;
        }
    }
}
