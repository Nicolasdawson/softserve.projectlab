using API.Models;

namespace API.Services;
public interface IPaymentRepository
{
    Payment CreatePayment(Payment payment);
    IEnumerable<Payment> GetAllPayments();
    Payment? GetPaymentById(Guid id);
    bool UpdatePayment(Guid id, Payment updated);
    bool DeletePayment(Guid id);
    void SavePaymentStatus(string intentId, string status);
    void SavePaymentStatusBySessionId(string sessionId, string status);
}
