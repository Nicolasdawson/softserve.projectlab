using API.Models.IntAdmin;
using API.Models.Logistics.Interfaces;

namespace API.Models.Logistics
{
    public class Warehouse : IWarehouse
    {
        public Warehouse(int wareHouseId, string name, string location, int capapcity, List<Item> items)
        {
            WareHouseId = wareHouseId;
            Name = name;
            Location = location;
            Capapcity = capapcity;
            Items = items;
        }

        public int WareHouseId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public int Capapcity { get; set; }
        public List<Item> Items { get; set; }

        public void AddItem(Item item)
        {
            if (Items.Count < Capapcity)
            {
                Items.Add(item);
            }
            else
            {
                throw new InvalidOperationException("Warehouse capacity exceeded.");
            }
        }

        public void RemoveItem(Item item)
        {
            Items.Remove(item);
        }

        public void GetAvailableStock(string sku)
        {
            var availableItems = Items.Where(item => item.Sku == sku && item.Stock > 0).ToList();
            if (!availableItems.Any())
            {
                Console.WriteLine("No available stock for this SKU.");
            }
            else
            {
                foreach (var item in availableItems)
                {
                    Console.WriteLine($"Item SKU: {item.Sku}, Stock: {item.Stock}");
                }
            }
        }
    }


}
