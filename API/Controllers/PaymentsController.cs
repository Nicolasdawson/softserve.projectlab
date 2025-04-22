using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Services;
using Stripe;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly PaymentService _paymentService;
        private readonly StripePaymentService _stripeService;

        public PaymentsController(PaymentService paymentService, StripePaymentService stripeService)
        {
            _paymentService = paymentService;
            _stripeService = stripeService;
        }

        // ---------------- CRUD interno ----------------

        [HttpGet]
        public ActionResult<IEnumerable<Payment>> GetPayments()
        {
            return Ok(_paymentService.GetAllPayments());
        }

        [HttpGet("{id}")]
        public ActionResult<Payment> GetPaymentById(Guid id)
        {
            var payment = _paymentService.GetPaymentById(id);
            if (payment == null)
            {
                return NotFound();
            }
            return Ok(payment);
        }

        [HttpPost]
        public ActionResult<Payment> CreatePayment(Payment payment)
        {
            var created = _paymentService.CreatePayment(payment);
            return CreatedAtAction(nameof(GetPaymentById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePayment(Guid id, Payment payment)
        {
            if (!_paymentService.UpdatePayment(id, payment))
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePayment(Guid id)
        {
            if (!_paymentService.DeletePayment(id))
            {
                return NotFound();
            }
            return NoContent();
        }

        // ---------------- Stripe integración ----------------

        [HttpPost("create-checkout-session")]
        public ActionResult CreateCheckoutSession([FromBody] CreatePaymentRequest request)
        {
            var session = _stripeService.CreateCheckoutSession(
                request.Amount,
                request.Currency,
                request.SuccessUrl,
                request.CancelUrl,
                request.CustomerEmail
            );

            return Ok(new { sessionId = session.Id, url = session.Url });
        }

        [HttpGet("session-details/{sessionId}")]
        public IActionResult GetSessionDetails(string sessionId)
        {
            try
            {
                var sessionService = new Stripe.Checkout.SessionService();
                var session = sessionService.Get(sessionId);

                return Ok(new
                {
                    SessionId = session.Id,
                    Email = session.CustomerEmail,
                    Status = session.PaymentStatus,
                    Amount = (session.AmountTotal ?? 0) / 100m,
                    Currency = session.Currency,
                    PaymentIntentId = session.PaymentIntentId
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("payment-details-from-charge/{paymentIntentId}")]
        public IActionResult GetPaymentDetailsFromCharge(string paymentIntentId)
        {
            try
            {
                var chargeService = new ChargeService();

                var options = new ChargeListOptions
                {
                    PaymentIntent = paymentIntentId,
                    Limit = 1,
                    Expand = new List<string>
                    {
                        "data.payment_method_details",
                        "data.billing_details"
                    }
                };

                var charges = chargeService.List(options);
                var charge = charges.Data.FirstOrDefault();
                var card = charge?.PaymentMethodDetails?.Card;

                return Ok(new
                {
                    Status = charge?.Status,
                    Amount = charge?.Amount / 100m,
                    Currency = charge?.Currency,
                    CardBrand = card?.Brand,
                    Last4 = card?.Last4,
                    ExpMonth = card?.ExpMonth,
                    ExpYear = card?.ExpYear,
                    CardHolderName = charge?.BillingDetails?.Name
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
