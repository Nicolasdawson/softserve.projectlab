using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace softserve.projectlabs.Shared.DTOs
{
    /// <summary>
    /// DTO for transferring items between warehouses.
    /// </summary>
    public class TransferRequestDto
    {
        /// <summary>
        /// The ID of the source warehouse.
        /// </summary>
        public int SourceWarehouseId { get; set; }

        /// <summary>
        /// The ID of the target warehouse.
        /// </summary>
        public int TargetWarehouseId { get; set; }

        /// <summary>
        /// The ID of the item to transfer.
        /// </summary>
        public int ItemId { get; set; }

        /// <summary>
        /// The quantity of the item to transfer.
        /// </summary>
        public int Quantity { get; set; }
    }
}
