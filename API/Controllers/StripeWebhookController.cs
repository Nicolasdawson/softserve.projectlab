using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using API.Services;
using API.Models;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StripeWebhookController : ControllerBase
{
    private readonly PaymentService _paymentService;
    private readonly IConfiguration _configuration;

    public StripeWebhookController(PaymentService paymentService, IConfiguration configuration)
    {
        _paymentService = paymentService;
        _configuration = configuration;
    }

    [HttpPost]
    public async Task<IActionResult> Post()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
        var endpointSecret = _configuration["Stripe:WebhookSecret"];

        try
        {
            var stripeEvent = EventUtility.ConstructEvent(
                json,
                Request.Headers["Stripe-Signature"],
                endpointSecret,
                60000 // Tolerancia de 10 minutos (solo para desarrollo)
            );

            Console.WriteLine($"Stripe event received: {stripeEvent.Type}");

            if (stripeEvent.Type == "checkout.session.completed")
            {
                Console.WriteLine("Procesando checkout.session.completed");

                var session = stripeEvent.Data.Object as Session;

                if (session != null)
                {
                    var payment = new Payment
                    {
                        Id = Guid.NewGuid(),
                        TransactionId = session.PaymentIntentId ?? "unknown",
                        Status = "paid",
                        ResponseCode = "200",
                        PaymentMethod = "card",
                        CardType = "unknown",
                        CardLastFour = "****",
                        ExpirationDate = "00/00",
                        Amount = session.AmountTotal.HasValue ? session.AmountTotal.Value / 100m : 0,
                        Currency = session.Currency,
                        CardHolderName = session.CustomerEmail ?? "Desconocido",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };

                    _paymentService.CreatePayment(payment);
                }
            }

            return Ok();
        }
        catch (StripeException e)
        {
            Console.WriteLine($"Stripe error: {e.Message}");
            return BadRequest(new { error = e.Message });
        }
    }
}
