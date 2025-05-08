using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using API.Services;
using API.Models;
using System.Text.Json;

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
        var endpointSecret = _configuration["Stripe:WebhookSecret"];
        string json;

        using (var reader = new StreamReader(HttpContext.Request.Body))
        {
            json = await reader.ReadToEndAsync();
        }

        try
        {
            var stripeEvent = EventUtility.ConstructEvent(
                json,
                Request.Headers["Stripe-Signature"],
                endpointSecret,
                60000
            );

            Console.WriteLine($"Stripe event received: {stripeEvent.Type}");

            switch (stripeEvent.Type)
            {
                case "checkout.session.completed":
                    Console.WriteLine("Processing checkout.session.completed");
                    var session = stripeEvent.Data.Object as Session;

                    if (session != null)
                    {
                        var payment = new Payment
                        {
                            Id = Guid.NewGuid(),
                            StripeSessionId = session.Id,
                            PaymentIntentId = session.PaymentIntentId,
                            Status = "succeeded",
                            Amount = session.AmountTotal.HasValue ? session.AmountTotal.Value / 100m : 0,
                            Currency = session.Currency,
                            CreatedAt = DateTime.UtcNow,
                        };

                        _paymentService.CreatePayment(payment);

                        if (!string.IsNullOrEmpty(session.CustomerEmail))
                        {
                            var emailBody = $"Thank you for your purchase! Your total was: {(session.AmountTotal ?? 0) / 100.0m} {session.Currency.ToUpper()}";
                            await _emailService.SendPaymentConfirmationEmail(
                                session.CustomerEmail,
                                "Payment Confirmation",
                                emailBody
                            );
                        }
                    }
                    break;

                case "payment_intent.payment_failed":
                    Console.WriteLine("Processing payment_intent.payment_failed");
                    var failedIntent = stripeEvent.Data.Object as PaymentIntent;
                    if (failedIntent != null)
                    {
                        _paymentService.SavePaymentStatus(failedIntent.Id, "failed");
                    }
                    break;

                case "payment_intent.canceled":
                    Console.WriteLine("Processing payment_intent.canceled");
                    var canceledIntent = stripeEvent.Data.Object as PaymentIntent;
                    if (canceledIntent != null)
                    {
                        _paymentService.SavePaymentStatus(canceledIntent.Id, "canceled");
                    }
                    break;

                default:
                    Console.WriteLine($"Unhandled event type: {stripeEvent.Type}");
                    break;
            }

            return Ok();
        }
        catch (StripeException e)
        {
            Console.WriteLine($"Stripe error: {e.Message}");
            return BadRequest(new { error = e.Message });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
            return StatusCode(500, new { error = "Internal server error", details = ex.Message });
        }
    }
}
