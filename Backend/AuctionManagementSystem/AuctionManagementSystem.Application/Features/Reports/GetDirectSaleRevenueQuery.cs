using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.TransactionsDtos;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Reports
{
    public class GetDirectSaleRevenueQuery : IRequest<List<DirectSaleMonthlyRevenueDto>>
    {
        public string ViewByMode { get; }

        public GetDirectSaleRevenueQuery(string viewByMode)
        {
            ViewByMode = viewByMode;
        }
    }

}
