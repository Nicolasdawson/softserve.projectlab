using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models.Logistics.Interfaces;
using API.Domain.Logistics;
using softserve.projectlabs.Shared.Utilities;
using softserve.projectlabs.Shared.Interfaces;
using softserve.projectlabs.Shared.DTOs;
using AutoMapper;
using API.Models.Logistics;

namespace API.Services.Logistics
{
    public class SupplierOrderService : ISupplierOrderService
    {
        private readonly SupplierOrderDomain _domain;
        private readonly IMapper _mapper;

        public SupplierOrderService(SupplierOrderDomain domain, IMapper mapper)
        {
            _domain = domain;
            _mapper = mapper;
        }

        public async Task<List<SupplierOrderDto>> GetAllSupplierOrdersAsync()
        {
            var orders = await _domain.GetAllSupplierOrdersAsync();

            // Map List<ISupplierOrder> to List<SupplierOrderDto>
            var orderDtos = orders.Select(order =>
            {
                var supplierOrder = new SupplierOrder(order.GetOrderData());
                return supplierOrder.GetOrderData();
            }).ToList();

            return orderDtos;
        }

        public async Task<SupplierOrderDto> GetSupplierOrderByIdAsync(int orderId)
        {
            var order = await _domain.GetSupplierOrderByIdAsync(orderId);

            // Create a new SupplierOrder instance
            var supplierOrder = new SupplierOrder(order.GetOrderData());

            // Use GetOrderData() to retrieve SupplierOrderDto
            return supplierOrder.GetOrderData();
        }

        public async Task<SupplierOrderDto> AddSupplierOrderAsync(SupplierOrderDto orderDto)
        {
            // Create a new SupplierOrder instance using SupplierOrderDto
            var supplierOrder = new SupplierOrder(orderDto);

            // Add the order to the domain
            var createdOrder = await _domain.AddSupplierOrderAsync(supplierOrder);

            // Use GetOrderData() to retrieve SupplierOrderDto
            return createdOrder.GetOrderData();
        }

        public async Task<bool> UpdateSupplierOrderAsync(SupplierOrderDto orderDto)
        {
            // Create a new SupplierOrder instance using SupplierOrderDto
            var supplierOrder = new SupplierOrder(orderDto);

            // Update the order in the domain
            return await _domain.UpdateSupplierOrderAsync(supplierOrder);
        }

        public async Task<bool> DeleteSupplierOrderAsync(int orderId)
        {
            return await _domain.DeleteSupplierOrderAsync(orderId);
        }
    }
}

