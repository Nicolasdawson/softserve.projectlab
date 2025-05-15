namespace API.Models.Logistics.Order
{
    public class OrderItem
    {
        public int Sku { get; private set; }
        public string ItemName { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }

        public OrderItem(int sku, string itemName, int quantity, decimal unitPrice)
        {
            Sku = sku;
            ItemName = itemName;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }
    }
}
