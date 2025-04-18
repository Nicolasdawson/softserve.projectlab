using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace API.Data.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Address { get; set; }

        [Required]
        [MaxLength(200)]
        public double OrderTotal { get; set; }

        [Required]
        public DateTime OrderPlaced { get; set; } = DateTime.Now;

        [Required]
        public int UserId { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
