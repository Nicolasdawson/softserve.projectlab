using System;
using System.ComponentModel.DataAnnotations;

namespace API.Models;

    public class Payment
    {
        public Guid Id { get; set; }

        [MaxLength(50)]
        public string TransactionId { get; set; } = default!;

        [MaxLength(20)]
        public string Status { get; set; } = default!;

        [MaxLength(10)]
        public string ResponseCode { get; set; } = default!;
        
        [MaxLength(20)]
        public string PaymentMethod { get; set; } = default!;

        [MaxLength(20)]
        public string CardType { get; set; } = default!;

        [MaxLength(4)]
        public string CardLastFour { get; set; } = default!;

        [MaxLength(7)]
        public string ExpirationDate { get; set; } = default!;

        [Range(0.01, double.MaxValue)]
        public decimal Amount { get; set; }

        [MaxLength(3)]
        public string Currency { get; set; } = default!;

        [MaxLength(100)]
        public string CardHolderName { get; set; } = default!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }

