using System;
using System.Threading;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts;
using AuctionManagementSystem.Application.Contracts.AuditTrail;
using AuctionManagementSystem.Application.Contracts.Auth;
using AuctionManagementSystem.Application.Contracts.User;
using AuctionManagementSystem.Application.Exceptions;
using AuctionManagementSystem.Domain.Entities.User;
using MediatR;
using Newtonsoft.Json;

namespace AuctionManagementSystem.Application.Features.UserFeature.Command.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuditTrailService _auditTrailService;
        private readonly ICurrentUserService _currentUser;

        public DeleteUserCommandHandler(
            IUserRepository userRepository,
            IAuditTrailService auditTrailService,
            ICurrentUserService currentUser)
        {
            _userRepository = userRepository;
            _auditTrailService = auditTrailService;
            _currentUser = currentUser;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserById(request.Id);
            if (user == null || user.IsDeleted.GetValueOrDefault())
                throw new NotFoundException("User not found");

            var oldUser = JsonConvert.DeserializeObject<TblUser>(JsonConvert.SerializeObject(user));

            user.IsDeleted = true;
            user.DeletedBy = _currentUser.UserId.ToString();
            user.DeletedDate = DateTime.UtcNow;

            var result = await _userRepository.DeleteUserAsync(user);

            if (result)
            {
                await _auditTrailService.LogChangeAsync(
                    userId: _currentUser.UserId,
                    username: _currentUser.Username,
                    roleName: _currentUser.RoleName,
                    modelName: "User",
                    changeType: "Delete",
                    recordId: user.UserId,
                    beforeChange: oldUser,
                    afterChange: null
                );

                Console.WriteLine($"CurrentUser: {_currentUser.UserId}, {_currentUser.Username}");
                Console.WriteLine($"Audit RoleName: {_currentUser.RoleName}");
            }

            return result;
        }
    }
}
