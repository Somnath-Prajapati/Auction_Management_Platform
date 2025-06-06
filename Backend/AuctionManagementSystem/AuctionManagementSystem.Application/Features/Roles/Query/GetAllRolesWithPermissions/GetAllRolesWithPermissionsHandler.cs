using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Roles;
using AuctionManagementSystem.Application.Contracts.User;
using AuctionManagementSystem.Application.Dtos.Roles;
using AutoMapper;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Roles.Query.GetAllRoles
{
    public class GetAllRolesWithPermissionsHandler : IRequestHandler<GetAllRolesWithPermissionsQuery, List<RoleWithPermissionsDto>>
    {
        private readonly IRoleRepository _roleRepo;
        private readonly IMapper _mapper;

        public GetAllRolesWithPermissionsHandler(
            IRoleRepository roleRepo,
            IMapper mapper)
        {
            _roleRepo = roleRepo;
            _mapper = mapper;
        }

        public async Task<List<RoleWithPermissionsDto>> Handle(GetAllRolesWithPermissionsQuery request, CancellationToken cancellationToken)
        {
            var roles = await _roleRepo.GetAllRoles();
            return _mapper.Map<List<RoleWithPermissionsDto>>(roles);
            
        }
    }
    
}
