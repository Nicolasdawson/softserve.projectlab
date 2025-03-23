using API.Models.IntAdmin;
namespace API.Models.Logistics.Interfaces
{
    public interface IWarehouse
    {
        int WareHouseId { get; set; }
        string Name { get; set; }
        string Location { get; set; }
        int Capapcity { get; set; }
        List<Item> Items { get; set; }

        Result<IWarehouse> AddItem(Item item);
        Result<IWarehouse> RemoveItem(Item item);
        Result<IWarehouse> GetAvailableStock(string sku);
    }
}
