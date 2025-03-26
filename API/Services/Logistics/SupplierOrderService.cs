using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models.Logistics.Interfaces;
using API.Domain.Logistics;

namespace API.Services.Logistics
{
    public class SupplierOrderService : ISupplierOrderService
    {
        private readonly SupplierOrderDomain _domain;

        public SupplierOrderService(SupplierOrderDomain domain)
        {
            _domain = domain;
        }

        public async Task<List<ISupplierOrder>> GetAllSupplierOrdersAsync()
        {
            return await _domain.GetAllSupplierOrdersAsync();
        }

        public async Task<ISupplierOrder> GetSupplierOrderByIdAsync(int orderId)
        {
            return await _domain.GetSupplierOrderByIdAsync(orderId);
        }

        public async Task<ISupplierOrder> AddSupplierOrderAsync(ISupplierOrder order)
        {
            return await _domain.AddSupplierOrderAsync(order);
        }

        public async Task<bool> UpdateSupplierOrderAsync(ISupplierOrder order)
        {
            return await _domain.UpdateSupplierOrderAsync(order);
        }

        public async Task<bool> DeleteSupplierOrderAsync(int orderId)
        {
            return await _domain.DeleteSupplierOrderAsync(orderId);
        }
    }
}
