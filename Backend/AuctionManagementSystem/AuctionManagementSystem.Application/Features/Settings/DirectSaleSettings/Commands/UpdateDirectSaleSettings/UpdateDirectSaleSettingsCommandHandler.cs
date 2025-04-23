using AuctionManagementSystem.Application.Dtos.Settings;
using AuctionManagementSystem.Domain.Entities.Settings;
using AuctionManagementSystem.Domain.Interfaces;
using AutoMapper;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Settings.DirectSaleSettings.Commands.UpdateDirectSaleSettings
{
    public class UpdateDirectSaleSettingsCommandHandler : IRequestHandler<UpdateDirectSaleSettingsCommand, DirectSaleSettingsDto>
    {
        private readonly IDirectSaleSettingsRepository _repository;
        private readonly IMapper _mapper;

        public UpdateDirectSaleSettingsCommandHandler(IDirectSaleSettingsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<DirectSaleSettingsDto> Handle(UpdateDirectSaleSettingsCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
                throw new Exception("Direct Sale setting not found.");

            entity.CartItemsLimit = request.CartItemsLimit;
            entity.CartTimerInMinutes = request.CartTimerInMinutes;

            await _repository.UpdateAsync(entity);

            return _mapper.Map<DirectSaleSettingsDto>(entity);
        }
    }
}
