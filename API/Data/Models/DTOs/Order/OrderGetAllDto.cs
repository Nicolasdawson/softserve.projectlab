using System;

namespace API.Data.Models.DTOs.Order
{
    public class OrderGetAllDto
    {
        public int Id { get; set; }

        public string Address { get; set; }

        public decimal OrderTotal { get; set; }

        public DateTime OrderPlaced { get; set; } = DateTime.Now;

        public int UserId { get; set; }
    }
}
