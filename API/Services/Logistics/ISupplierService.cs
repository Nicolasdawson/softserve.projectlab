using API.Models;
using Logistics.Models;

namespace API.Services.Logistics
{
    public interface ISupplierService
    {
        Task<Result<Supplier>> CreateSupplierAsync(Supplier supplier);
        Task<Result<Supplier>> GetSupplierByIdAsync(int supplierId);
        Task<Result<List<Supplier>>> GetAllSuppliersAsync();
        Task<Result<Supplier>> UpdateSupplierAsync(Supplier supplier);
        Task<Result<bool>> DeleteSupplierAsync(int supplierId);
    }
}
