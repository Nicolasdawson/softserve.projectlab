using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Services;

namespace API.Controllers;

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


        [HttpPost]
        public ActionResult<Payment> CreatePayment(Payment payment)
        {
            var created = _paymentService.CreatePayment(payment);
            return CreatedAtAction(nameof(GetPaymentById), new { id = created.Id }, created);
        }

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

        [HttpPost("create-checkout-session")]
        public ActionResult CreateCheckoutSession([FromBody] CreatePaymentRequest request)
        {
            var session = _stripeService.CreateCheckoutSession(
                request.Amount,
                request.Currency,
                request.SuccessUrl,
                request.CancelUrl
            );

            return Ok(new { sessionId = session.Id, url = session.Url });
        }

    }

