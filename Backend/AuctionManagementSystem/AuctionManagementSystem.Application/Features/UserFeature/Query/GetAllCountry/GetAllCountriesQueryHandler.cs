using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.User;
using AuctionManagementSystem.Domain.Entities.User;
using MediatR;

namespace AuctionManagementSystem.Application.Features.UserFeature.Query.GetAllCountry
{
    public class GetAllCountriesQueryHandler : IRequestHandler<GetAllCountriesQuery, IEnumerable<TblCountry>>
    {
        private readonly ICountryRepository _countryRepository;

        public GetAllCountriesQueryHandler(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<IEnumerable<TblCountry>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
        {
            return await _countryRepository.GetAllCountries();
        }
    }
}