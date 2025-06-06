using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.User;
using AuctionManagementSystem.Domain.Entities.Roles;
using AuctionManagementSystem.Domain.Entities.User;
using AutoMapper;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Roles.Command.CreateRoles
{
    public class CreateRoleWithPermissionsHandler : IRequestHandler<CreateRoleWithPermissionsCommand, int>
    {
        private readonly IRoleRepository _roleRepo;
        private readonly IMapper _mapper;

        public CreateRoleWithPermissionsHandler(IRoleRepository roleRepo, IMapper mapper)
        {
            _roleRepo = roleRepo;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateRoleWithPermissionsCommand request, CancellationToken cancellationToken)
        {
            var roleEntity = _mapper.Map<TblRole>(request.RoleDto);
            await _roleRepo.AddRoleAsync(roleEntity);

            var permissionsEntity = _mapper.Map<TblRolePermissionsMatrix>(request.RoleDto);
            permissionsEntity.RoleId = roleEntity.RoleId;
            await _roleRepo.AddRolePermissionsAsync(permissionsEntity);

            return roleEntity.RoleId;
        }
    }

}
