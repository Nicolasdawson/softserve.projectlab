using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace API.Data.Models.DTOs.Order
{
    public class OrderPostDto
    {
        [Required]
        public string Address { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
