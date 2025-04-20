using API.Models.IntAdmin;
using softserve.projectlabs.Shared.Utilities;

namespace API.Models.Logistics.Interfaces
{
    public interface IOrder
    {
        // Business logic methods
        Result<IOrder> AddOrder(IOrder order);
        Result<IOrder> UpdateOrder(IOrder order);
        Result<IOrder> GetOrderById(int orderId);
        Result<List<IOrder>> GetAllOrders();
        Result<bool> RemoveOrder(int orderId);
        Result<bool> AddItemToOrder(Item item);
        Result<bool> RemoveItemFromOrder(Item item);
    }
}
