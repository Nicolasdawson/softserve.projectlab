using API.Models.IntAdmin;
using softserve.projectlabs.Shared.Utilities;

namespace API.Models.Logistics.Interfaces
{
    public interface ISupplier
    {
        // Business logic methods
        Result<ISupplier> AddSupplier(ISupplier supplier);
        Result<ISupplier> UpdateSupplier(ISupplier supplier);
        Result<ISupplier> GetSupplierById(int supplierId);
        Result<List<ISupplier>> GetAllSuppliers();
        Result<bool> DeleteSupplier(int supplierId);
        Result<bool> AddProductToSupplier(Item item);
        Result<bool> RemoveProductFromSupplier(Item item);
        Result<List<Item>> GetSupplierProducts();
        Result<bool> CancelOrder(int orderId);
        Result<bool> CheckSupplierAvailability();
        Result<bool> UpdateSupplierStatus(bool isActive);
        Result<bool> RateSupplier(int rating, string feedback);
        Result<int> GetSupplierRating();
    }
}

