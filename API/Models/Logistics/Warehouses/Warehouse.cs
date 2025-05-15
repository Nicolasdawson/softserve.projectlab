using API.Models.IntAdmin;

namespace API.Models.Logistics.Warehouses
{
    public class Warehouse
    {
        public int WarehouseId { get; private set; }
        public string Name { get; private set; }
        public string Location { get; private set; }
        public int Capacity { get; private set; }
        public int BranchId { get; private set; }
        public List<Item> Items { get; private set; } = new();

        public Warehouse(int warehouseId, string name, string location, int capacity, int branchId, List<Item>? items = null)
        {
            WarehouseId = warehouseId;
            Name = name;
            Location = location;
            Capacity = capacity;
            BranchId = branchId;
            if (items != null)
                Items = items;
        }

        public void AddItem(Item item)
        {
            var existing = Items.FirstOrDefault(i => i.Sku == item.Sku);
            if (existing != null)
                existing.CurrentStock += item.CurrentStock;
            else
                Items.Add(item);
        }

        public void RemoveItem(int sku)
        {
            var item = Items.FirstOrDefault(i => i.Sku == sku);
            if (item != null)
                Items.Remove(item);
        }

        public void TransferItem(int sku, int quantity, Warehouse targetWarehouse)
        {
            var item = Items.FirstOrDefault(i => i.Sku == sku);
            if (item == null || item.CurrentStock < quantity)
                throw new InvalidOperationException("Not enough stock to transfer.");

            item.CurrentStock -= quantity;
            targetWarehouse.AddItem(new Item
            {
                Sku = item.Sku,
                ItemName = item.ItemName,
                ItemDescription = item.ItemDescription,
                CurrentStock = quantity
            });
        }
    }
}
