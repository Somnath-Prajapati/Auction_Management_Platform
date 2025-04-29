using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Domain.Entities.User;
using MediatR;

namespace AuctionManagementSystem.Application.Features.UserFeature.Query.GetAllCountry
{
    public record GetAllCountriesQuery : IRequest<IEnumerable<TblCountry>>;
}
