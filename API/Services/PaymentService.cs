using API.implementations.Infrastructure.Data;
using API.Models;

namespace API.Services
{
    public class PaymentService
    {
        private readonly AppDbContext _context;

        public PaymentService(AppDbContext context)
        {
            _context = context;
        }

        public Payment CreatePayment(Payment payment)
        {
            payment.Id = Guid.NewGuid();
            payment.CreatedAt = DateTime.UtcNow;

            _context.Payments.Add(payment);
            _context.SaveChanges();
            return payment;
        }

        public IEnumerable<Payment> GetAllPayments()
        {
            return _context.Payments.ToList();
        }

        public Payment? GetPaymentById(Guid id)
        {
            return _context.Payments.FirstOrDefault(p => p.Id == id);
        }

        public bool DeletePayment(Guid id)
        {
            var payment = GetPaymentById(id);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public bool UpdatePayment(Guid id, Payment updatedPayment)
        {
            var existing = GetPaymentById(id);
            if (existing != null)
            {
                existing.StripeSessionId = updatedPayment.StripeSessionId;
                existing.Status = updatedPayment.Status;
                existing.Amount = updatedPayment.Amount;
                existing.Currency = updatedPayment.Currency;
                // createdAt no se cambia, así que lo dejamos igual

                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
