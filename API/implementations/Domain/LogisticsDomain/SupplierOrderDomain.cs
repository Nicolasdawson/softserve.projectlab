using API.Models.Logistics.LogisticsInterface;
using API.Models;
using Logistics.Models;

namespace API.Domain.Logistics
{
    public class SupplierOrderDomain
    {
        private readonly ISupplierOrder _supplierOrderService;

        public SupplierOrderDomain(ISupplierOrder supplierOrderService)
        {
            _supplierOrderService = supplierOrderService;
        }

        // Domain logic for creating, updating, and deleting supplier orders
        public Result<ISupplierOrder> CreateOrder(SupplierOrder order)
        {
            // Additional business logic
            return _supplierOrderService.AddSupplierOrder(order as ISupplierOrder);
        }

        // Additional domain methods can be added here for validation or logic
    }
}
