using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Application.Dtos.Payment;
using MediatR;

namespace AuctionManagementSystem.Application.Features.Listings.Payment
{
    public class CreateStripeCheckoutSessionCommand : IRequest<string>
    {
        public CreateStripeSessionDto StripeSessionDto { get; set; }
    }

    public class ConfirmStripePaymentAndCreateOrderCommand : IRequest<List<DirectSaleAssetDto>>
    {
        public ConfirmStripePaymentDto PaymentDto { get; set; }
    }

    public class CheckStripePaymentStatusQuery : IRequest<string>
    {
        public string SessionId { get; set; }
    }


}
