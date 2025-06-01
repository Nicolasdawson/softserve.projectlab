using API.Models;
using Stripe.Checkout;

namespace API.Services;

public interface IPaymentService
{
    Task<Payment> CreatePaymentAsync(Guid orderId, decimal amount);
    Session CreateCheckoutSession(decimal amount, string currency, string successUrl, string cancelUrl, string? customerEmail);
}
