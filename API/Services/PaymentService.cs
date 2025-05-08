using API.implementations.Infrastructure.Data;
using API.Models;

namespace API.Services;

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

    public void SavePaymentStatus(string paymentIntentId, string status)
    {
        var payment = _context.Payments.FirstOrDefault(p => p.PaymentIntentId == paymentIntentId);
        if (payment != null)
        {
            payment.Status = status;
        }
        else
        {
            // Creamos un pago mínimo para registrar el intento
            payment = new Payment
            {
                Id = Guid.NewGuid(),
                PaymentIntentId = paymentIntentId,
                Status = status,
                StripeSessionId = "",
                Amount = 0,
                Currency = "usd", // valor por defecto, puedes ajustarlo
                CreatedAt = DateTime.UtcNow
            };
            _context.Payments.Add(payment);
        }

        _context.SaveChanges();
    }

    public void SavePaymentStatusBySessionId(string sessionId, string status)
    {
        var payment = _context.Payments.FirstOrDefault(p => p.StripeSessionId == sessionId);
        if (payment != null)
        {
            payment.Status = status;
            _context.SaveChanges();
        }
    }
}
