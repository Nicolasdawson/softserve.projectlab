using API.Models.Logistics.LogisticsInterface;
using API.Models;
using Logistics.Models;

namespace API.Domain.Logistics
{
    public class SupplierOrderDomain
    {
        private readonly ISupplierOrder _supplierOrderService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SupplierOrderDomain"/> class.
        /// </summary>
        /// <param name="supplierOrderService">The supplier order service.</param>
        public SupplierOrderDomain(ISupplierOrder supplierOrderService)
        {
            _supplierOrderService = supplierOrderService;
        }

        /// <summary>
        /// Creates a new supplier order.
        /// </summary>
        /// <param name="order">The supplier order to create.</param>
        /// <returns>A result containing the created supplier order.</returns>
        public Result<ISupplierOrder> CreateOrder(SupplierOrder order)
        {
            // Additional business logic
            return _supplierOrderService.AddSupplierOrder(order as ISupplierOrder);
        }

        // Additional domain methods can be added here for validation or logic
    }
}
