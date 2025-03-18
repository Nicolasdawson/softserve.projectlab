using API.Models.IntAdmin;
using API.Models.Logistics.Interfaces;
using API.Models;


namespace API.Services.Logistics
{
    public interface IWarehouseService
    {
        Result<IWarehouse> AddItem(Item item);
        Result<IWarehouse> RemoveItem(Item item);
        Result<IWarehouse> GetAvailableStock(string sku);
    }
}
