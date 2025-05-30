using Stripe.Checkout;
using API.Models;

namespace API.Services
{
    public class StripePaymentService : IPaymentService
    {
        public Task<Payment> CreatePaymentAsync(Guid orderId, decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Teh amount must be more than 0");

            var session = CreateCheckoutSession(
                amount,
                "usd",
                "https://example.com/success", // poné tus URLs reales
                "https://example.com/cancel",
                null
            );

            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                StripeSessionId = session.Id,
                PaymentIntentId = session.PaymentIntentId,
                Amount = amount,
                Currency = "usd",
                Status = "unpaid",
                CreatedAt = DateTime.UtcNow,
                IdOrder = orderId
            };

            return Task.FromResult(payment);
        }

        public Session CreateCheckoutSession(decimal amount, string currency, string successUrl, string cancelUrl, string? customerEmail)
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmountDecimal = amount * 100,
                            Currency = currency,
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Compra demo"
                            }
                        },
                        Quantity = 1
                    }
                },
                Mode = "payment",
                SuccessUrl = successUrl,
                CancelUrl = cancelUrl,
                CustomerEmail = customerEmail
            };

            var service = new SessionService();
            return service.Create(options);
        }
    }
}
