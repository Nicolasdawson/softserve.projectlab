using API.Models.IntAdmin;
using API.Models.Logistics.Interfaces;

namespace API.Models.Logistics
{
    public class Order : IOrder
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public List<Item> Items { get; set; }
        public string Status { get; set; }

        public Order(int orderId, int customerId, DateTime orderDate, List<Item> items, string status)
        {
            OrderId = orderId;
            CustomerId = customerId;
            OrderDate = orderDate;
            Items = items;
            Status = status;
        }

        public Result<IOrder> AddOrder(IOrder order)
        {
            // Logic for adding a new order (e.g., saving to database or collection)
            return Result<IOrder>.Success(order);
        }

        public Result<IOrder> UpdateOrder(IOrder order)
        {
            // Logic for updating an existing order
            return Result<IOrder>.Success(order);
        }

        public Result<IOrder> GetOrderById(int orderId)
        {
            // Logic to retrieve an order by its ID
            var order = new Order(orderId, 1, DateTime.Now, new List<Item>(), "Pending");
            return Result<IOrder>.Success(order);
        }

        public Result<List<IOrder>> GetAllOrders()
        {
            // Logic to get all orders
            var orders = new List<IOrder>
            {
                new Order(1, 1, DateTime.Now, new List<Item>(), "Shipped"),
                new Order(2, 2, DateTime.Now.AddDays(-1), new List<Item>(), "Pending")
            };
            return Result<List<IOrder>>.Success(orders);
        }

        public Result<bool> RemoveOrder(int orderId)
        {
            // Logic to remove an order
            return Result<bool>.Success(true); // Assume the order was removed successfully
        }

        public Result<bool> AddItemToOrder(Item item)
        {
            // Logic to add an item to an order
            Items.Add(item);
            return Result<bool>.Success(true);
        }

        public Result<bool> RemoveItemFromOrder(Item item)
        {
            // Logic to remove an item from an order
            Items.Remove(item);
            return Result<bool>.Success(true);
        }
    }
}
