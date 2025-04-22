using Stripe.Checkout;

namespace API.Services;

    public class StripePaymentService
    {
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

