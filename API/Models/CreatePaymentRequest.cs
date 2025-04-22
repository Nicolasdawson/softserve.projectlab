namespace API.Models;

    public class CreatePaymentRequest
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "usd";
        public string SuccessUrl { get; set; } = default!;
        public string CancelUrl { get; set; } = default!;
        public string? CustomerEmail { get; set; }
    }

