using API.Abstractions;

namespace API.Models
{
    public class Payment : Base
    {
        public string TransactionId { get; set; } = default!;
        public string Status { get; set; } = default!;
        public string ResponseCode { get; set; } = default!;
        public string WebpayToken { get; set; } = default!;
        public string PaymentMethod { get; set; } = default!;
        public string CardType { get; set; } = default!;
        public string CardLastFour { get; set; } = default!;
        public string ExpirationDate { get; set; } = default!;
        public decimal Amount { get; set; }
        public string Currency { get; set; } = default!;
        public string CardHolderName { get; set; } = default!;
    }
}
