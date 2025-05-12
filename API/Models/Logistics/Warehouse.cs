using API.Models.IntAdmin;
using API.Models.Logistics.Interfaces;
using softserve.projectlabs.Shared.DTOs;

namespace API.Models.Logistics
{
    public class Warehouse : IWarehouse
    {
        private readonly WarehouseDto _warehouseDto;

        public Warehouse(WarehouseDto warehouseDto)
        {
            _warehouseDto = warehouseDto ?? throw new ArgumentNullException(nameof(warehouseDto));
        }

        public List<Item> Items { get; set; } = new List<Item>();

        public WarehouseDto GetWarehouseData() => _warehouseDto;

        public void AddItem(Item item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            Items.Add(item);
        }

        public void RemoveItem(int sku)
        {
            var item = Items.FirstOrDefault(i => i.Sku == sku);
            if (item == null) throw new InvalidOperationException("Item not found.");
            Items.Remove(item);
        }

        public void UpdateItemStock(int sku, int quantity)
        {
            var item = Items.FirstOrDefault(i => i.Sku == sku);
            if (item == null) throw new InvalidOperationException("Item not found.");
            item.CurrentStock += quantity;
        }

        public bool IsItemInStock(int sku, int requiredQuantity)
        {
            var item = Items.FirstOrDefault(i => i.Sku == sku);
            return item != null && item.CurrentStock >= requiredQuantity;
        }

        public void TransferItem(int sku, int quantity, IWarehouse targetWarehouse)
        {
            if (targetWarehouse == null) throw new ArgumentNullException(nameof(targetWarehouse));

            var item = Items.FirstOrDefault(i => i.Sku == sku);
            if (item == null || item.CurrentStock < quantity)
                throw new InvalidOperationException("Insufficient stock or item not found.");

            item.CurrentStock -= quantity;

            var targetItem = targetWarehouse.Items.FirstOrDefault(i => i.Sku == sku);
            if (targetItem == null)
            {
                targetWarehouse.AddItem(new Item
                {
                    Sku = sku,
                    CurrentStock = quantity
                });
            }
            else
            {
                targetItem.CurrentStock += quantity;
            }
        }
    }
}
