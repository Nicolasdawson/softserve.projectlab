using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models.Logistics.Interfaces;


namespace API.Services.Logistics
{
    public interface ISupplierOrderService
    {
        Task<List<ISupplierOrder>> GetAllSupplierOrdersAsync();
        Task<ISupplierOrder> GetSupplierOrderByIdAsync(int orderId);
        Task<ISupplierOrder> AddSupplierOrderAsync(ISupplierOrder order);
        Task<bool> UpdateSupplierOrderAsync(ISupplierOrder order);
        Task<bool> DeleteSupplierOrderAsync(int orderId);
    }
}
