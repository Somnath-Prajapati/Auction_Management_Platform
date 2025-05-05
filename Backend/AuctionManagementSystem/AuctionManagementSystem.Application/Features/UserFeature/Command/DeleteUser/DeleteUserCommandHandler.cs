using System;
using System.Threading;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.User;
using AuctionManagementSystem.Application.Exceptions;
using MediatR;

namespace AuctionManagementSystem.Application.Features.UserFeature.Command.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserById(request.Id);
            if (user == null)
                throw new NotFoundException("User not found");

            user.IsDeleted = true;
            user.DeletedBy = "Admin";
            user.DeletedDate = DateTime.UtcNow;

            return await _userRepository.DeleteUserAsync(user);
        }
    }
}
