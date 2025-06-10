using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.Listings;
using AuctionManagementSystem.Application.Dtos.Assets;
using AuctionManagementSystem.Application.Features.Listings.Payment;
using MediatR;
using Stripe;
using Stripe.BillingPortal;
using Stripe.Checkout;
using SessionCreateOptions = Stripe.Checkout.SessionCreateOptions;

namespace AuctionManagementSystem.Application.Features.Listings.Stripe_Payment
{
    public class CreateStripeCheckoutSessionHandler : IRequestHandler<CreateStripeCheckoutSessionCommand, string>
    {
        public async Task<string> Handle(CreateStripeCheckoutSessionCommand request, CancellationToken cancellationToken)
        {
            var dto = request.StripeSessionDto;
            var options = new SessionCreateOptions
            {
                //PaymentMethodTypes = new List<string> { "card", "klarna", "ideal" },
                CustomerEmail = dto.email,
                Mode = "payment",
                LineItems = new List<SessionLineItemOptions>
    {
        new SessionLineItemOptions
        {
            PriceData = new SessionLineItemPriceDataOptions
            {
                Currency = "usd",
                UnitAmount = (long)(dto.TotalAmount * 100),
                ProductData = new SessionLineItemPriceDataProductDataOptions
                {
                    Name = "Auction Assets"
                }
            },
            Quantity = 1
        }
    },
                SuccessUrl = $"http://localhost:4200/payment-success?session_id={{{{CHECKOUT_SESSION_ID}}}}&userId={dto.UserId}",
                CancelUrl = $"http://localhost:4200/bid-add-to-cart",
                Metadata = new Dictionary<string, string>
                {
                    ["userId"] = dto.UserId.ToString(),
                    ["assetIds"] = string.Join(",", dto.AssetIds)
                }
            };



            var service = new Stripe.Checkout.SessionService();
            var session = await service.CreateAsync(options);

            return session.Id;
        }
    }

    public class ConfirmStripePaymentAndCreateOrderHandler : IRequestHandler<ConfirmStripePaymentAndCreateOrderCommand, List<DirectSaleAssetDto>>
    {
        private readonly IOrderRepository _orderRepository;

        public ConfirmStripePaymentAndCreateOrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<DirectSaleAssetDto>> Handle(ConfirmStripePaymentAndCreateOrderCommand request, CancellationToken cancellationToken)
        {
            Console.WriteLine("ConfirmStripePaymentAndCreateOrderHandler called");

            var dto = request.PaymentDto;

            var service = new Stripe.Checkout.SessionService();
            var session = await service.GetAsync(dto.SessionId, new Stripe.Checkout.SessionGetOptions
            {
                Expand = new List<string> { "line_items" }
            });


            if (session.PaymentStatus != "paid")
                throw new Exception("Stripe payment not completed.");

            var assetIds = session.Metadata["assetIds"]
                .Split(',')
                .Select(int.Parse)
                .ToList();

            var amountPaid = session.LineItems.Data[0].AmountTotal; // in cents

            // Fetch PaymentIntent to get the actual payment method
            var paymentIntentService = new Stripe.PaymentIntentService();
            var paymentIntent = await paymentIntentService.GetAsync(session.PaymentIntentId);

            var paymentMethodService = new Stripe.PaymentMethodService();
            var stripePaymentMethod = await paymentMethodService.GetAsync(paymentIntent.PaymentMethodId);

            var paymentMethodType = stripePaymentMethod.Type; // e.g., "card", "apple_pay", "google_pay"

            // Pass it to repository
            return await _orderRepository.ConfirmPaymentAndCreateOrderAsync(dto.UserId,assetIds,paymentMethodType,amountPaid);

        }

    }

    public class CheckStripePaymentStatusQueryHandler : IRequestHandler<CheckStripePaymentStatusQuery, string>
    {
        public async Task<string> Handle(CheckStripePaymentStatusQuery request, CancellationToken cancellationToken)
        {
            var service = new Stripe.Checkout.SessionService();
            var session = await service.GetAsync(request.SessionId);

            return session.PaymentStatus; // returns "paid", "unpaid", "no_payment_required", etc.
        }
    }



}
