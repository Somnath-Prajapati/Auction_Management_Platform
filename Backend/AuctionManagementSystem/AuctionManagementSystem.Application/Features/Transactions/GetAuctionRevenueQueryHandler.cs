//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using AuctionManagementSystem.Application.Contracts.Reports;
//using AuctionManagementSystem.Application.Contracts.Transactions;
//using AuctionManagementSystem.Application.Dtos.TransactionsDtos;
//using MediatR;

//namespace AuctionManagementSystem.Application.Features.Transactions
//{
//    public class GetAuctionRevenueQueryHandler : IRequestHandler<GetAuctionRevenueQuery, List<AuctionMonthlyRevenueDto>>
//    {
//        //private readonly ITransactionRepository _transactionRepository;
//        private readonly IReports _reports;

//        public GetAuctionRevenueQueryHandler(IReports reports)
//        {
//            //_transactionRepository = transactionRepository;
//            _reports = reports;
//        }

//        public async Task<List<AuctionMonthlyRevenueDto>> Handle(GetAuctionRevenueQuery request, CancellationToken cancellationToken)
//        {
//            return await _reports.GetAuctionRevenueAsync(request.ViewByMode);
//        }
//    }

//}
