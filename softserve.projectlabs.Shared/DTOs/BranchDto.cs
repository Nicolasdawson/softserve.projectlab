using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace softserve.projectlabs.Shared.DTOs
{
    public class BranchDto
    {
        public int BranchId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string ContactNumber { get; set; }
        public string ContactEmail { get; set; }
        public string Address { get; set; }
    }
}
