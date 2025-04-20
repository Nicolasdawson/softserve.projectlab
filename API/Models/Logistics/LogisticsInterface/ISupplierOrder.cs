using System.Collections.Generic;
using softserve.projectlabs.Shared.DTOs;

namespace API.Models.Logistics.Interfaces
{
    public interface ISupplierOrder
    {

        int OrderId { get; set; }
        // Business logic methods
        void UpdateStatus(string newStatus);
        void AddItem(OrderItemDto item);
        void RemoveItem(OrderItemDto item);
        decimal CalculateTotalAmount();

        SupplierOrderDto GetOrderData();

    }
}

