using System;
using System.Threading;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Settings;
using AuctionManagementSystem.Application.Repositories;
using AutoMapper;
using MediatR;
using AuctionManagementSystem.Domain.Entities.Settings;

namespace AuctionManagementSystem.Application.Features.Settings.FooterLinksSettings.Command.UpdateFooterLinksSettings
{
    public class UpdateFooterLinksSettingsCommandHandler : IRequestHandler<UpdateFooterLinksSettingsCommand, FooterLinksSettingsDto>
    {
        private readonly IFooterLinksSettingsRepository _repository;
        private readonly IMapper _mapper;

        public UpdateFooterLinksSettingsCommandHandler(IFooterLinksSettingsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<FooterLinksSettingsDto> Handle(UpdateFooterLinksSettingsCommand request, CancellationToken cancellationToken)
        {
            // Fetch the existing entity by Id
            var existing = await _repository.GetByIdAsync(request.Id);
            if (existing == null) throw new KeyNotFoundException("Footer links setting not found.");

            // Map the request's DTO to the existing entity to update it
            _mapper.Map(request.FooterLinksSettings, existing);

            // Update the entity in the repository
            var updated = await _repository.UpdateAsync(existing);

            // Return the updated entity as a DTO
            return _mapper.Map<FooterLinksSettingsDto>(updated);
        }
    }
}
