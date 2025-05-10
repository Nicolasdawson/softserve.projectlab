using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace API.Data.Models.DTOs.Order
{
    public class OrderGetByUserDto
    {
        public int Id { get; set; }
          
        public string Address { get; set; }

        public decimal OrderTotal { get; set; }

        public DateTime OrderPlaced { get; set; } = DateTime.Now;

    }
}
