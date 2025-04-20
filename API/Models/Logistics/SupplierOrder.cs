using API.Models.Logistics.Interfaces;
using softserve.projectlabs.Shared.DTOs;

public class SupplierOrder : ISupplierOrder
{
    private readonly SupplierOrderDto _orderDto;

    public SupplierOrder(int supplierId, SupplierOrderDto orderDto)
    {
        _orderDto = orderDto;
    }

    public SupplierOrder(SupplierOrderDto orderDto)
    {
        _orderDto = orderDto;
    }

    public int OrderId
    {
        get => _orderDto.OrderId;
        set => _orderDto.OrderId = value;
    }

    public SupplierOrderDto GetOrderData()
    {
        return _orderDto;
    }

    public void UpdateStatus(string newStatus)
    {
        _orderDto.Status = newStatus;
    }

    public void AddItem(OrderItemDto item)
    {
        _orderDto.Items.Add(item);
    }

    public void RemoveItem(OrderItemDto item)
    {
        _orderDto.Items.Remove(item);
    }

    public decimal CalculateTotalAmount()
    {
        return _orderDto.Items.Sum(item => item.TotalPrice);
    }
}
