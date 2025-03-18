namespace API.Models.Logistics.Interfaces
{
    public interface IWarehouse
    {
        int WareHouseId { get; set; }
        string Name { get; set; }
        string Location { get; set; }
        int Capapcity { get; set; }
        List<Item> Items { get; set; }

        void AddItem(Item item);
        void RemoveItem(Item item);
        void GetAvailableStock(string sku);
    }
}
