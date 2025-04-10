using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace softserve.projectlabs.Shared.DTOs
{
    public class OrderItemRequestDto
    {
        public int ItemId { get; set; }
        public int Quantity { get; set; }
    }
}
