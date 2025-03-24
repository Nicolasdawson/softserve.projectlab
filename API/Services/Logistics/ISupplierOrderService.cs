using API.Models;
using API.Models.Logistics.Interfaces;
using API.Models.Logistics.LogisticsInterface;
using System.Collections.Generic;

namespace API.Services.Logistics
{
    public interface ISupplierOrderService
    {
        Result<ISupplierOrder> AddSupplierOrder(ISupplierOrder supplierOrder);
        Result<ISupplierOrder> UpdateSupplierOrder(ISupplierOrder supplierOrder);
        Result<ISupplierOrder> GetSupplierOrderById(int orderId);
        Result<List<ISupplierOrder>> GetAllSupplierOrders();
        Result<bool> DeleteSupplierOrder(int orderId);
    }
}
