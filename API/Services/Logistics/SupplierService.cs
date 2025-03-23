using API.Implementations.Domain;
using API.Models;
using Logistics.Models;

namespace API.Services.Logistics
{
    public class SupplierService : ISupplierService
    {
        private readonly SupplierDomain _supplierDomain;

        public SupplierService(SupplierDomain supplierDomain)
        {
            _supplierDomain = supplierDomain;
        }

        public async Task<Result<Supplier>> CreateSupplierAsync(Supplier supplier)
        {
            return await _supplierDomain.CreateSupplier(supplier);
        }

        public async Task<Result<Supplier>> GetSupplierByIdAsync(int supplierId)
        {
            return await _supplierDomain.GetSupplierById(supplierId);
        }

        public async Task<Result<List<Supplier>>> GetAllSuppliersAsync()
        {
            return await _supplierDomain.GetAllSuppliers();
        }

        public async Task<Result<Supplier>> UpdateSupplierAsync(Supplier supplier)
        {
            return await _supplierDomain.UpdateSupplier(supplier);
        }

        public async Task<Result<bool>> DeleteSupplierAsync(int supplierId)
        {
            return await _supplierDomain.RemoveSupplier(supplierId);
        }
    }
}
