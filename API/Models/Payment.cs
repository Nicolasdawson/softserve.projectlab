using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Payment
    {
        public Guid Id { get; set; }

        [MaxLength(255)]
        public string StripeSessionId { get; set; } = default!;

        [MaxLength(50)]
        public string? PaymentIntentId { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "pending";

        [Range(0.01, double.MaxValue)]
        public decimal Amount { get; set; }

        [MaxLength(3)]
        public string Currency { get; set; } = default!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // 🔗 Relación 1:1 con Order
        [Required]
        public Guid IdOrder { get; set; }

        [ForeignKey(nameof(IdOrder))]
        public Order Order { get; set; } = default!;
    }
}
