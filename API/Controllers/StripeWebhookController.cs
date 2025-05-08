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

    private readonly EmailService _emailService;

    public StripeWebhookController(PaymentService paymentService, IConfiguration configuration, EmailService emailService)
    {
        _paymentService = paymentService;
        _configuration = configuration;
        _emailService = emailService;
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
                60000 // Tolerancia  (just for dev)
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
                        StripeSessionId  = session.Id,
                        PaymentIntentId = session.PaymentIntentId,
                        Status = "paid",
                        Amount = session.AmountTotal.HasValue ? session.AmountTotal.Value / 100m : 0,
                        Currency = session.Currency,
                        CreatedAt = DateTime.UtcNow,
                    };

                    _paymentService.CreatePayment(payment);

                    if (session.CustomerEmail != null)
                    {
                        var emailBody = $"Thank you for your purchase! Your total was: {(session.AmountTotal ?? 0) / 100.0m} {session.Currency.ToUpper()}";
                        await _emailService.SendPaymentConfirmationEmail(
                            session.CustomerEmail,
                            "Payment Confirmation",
                            emailBody
                        );
                    }
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
