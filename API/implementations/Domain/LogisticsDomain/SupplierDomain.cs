using API.Models;
using Logistics.Models;

namespace API.Implementations.Domain
{
    public class SupplierDomain
    {
        private readonly List<Supplier> _suppliers = new List<Supplier>(); // Example in-memory storage for suppliers

        public async Task<Result<Supplier>> CreateSupplier(Supplier supplier)
        {
            try
            {
                _suppliers.Add(supplier);
                return Result<Supplier>.Success(supplier);
            }
            catch (Exception ex)
            {
                return Result<Supplier>.Failure($"Failed to create supplier: {ex.Message}");
            }
        }

        public async Task<Result<Supplier>> GetSupplierById(int supplierId)
        {
            try
            {
                var supplier = _suppliers.FirstOrDefault(s => s.SupplierId == supplierId);
                return supplier != null ? Result<Supplier>.Success(supplier) : Result<Supplier>.Failure("Supplier not found.");
            }
            catch (Exception ex)
            {
                return Result<Supplier>.Failure($"Failed to retrieve supplier: {ex.Message}");
            }
        }

        public async Task<Result<List<Supplier>>> GetAllSuppliers()
        {
            try
            {
                return Result<List<Supplier>>.Success(_suppliers);
            }
            catch (Exception ex)
            {
                return Result<List<Supplier>>.Failure($"Failed to retrieve suppliers: {ex.Message}");
            }
        }

        public async Task<Result<Supplier>> UpdateSupplier(Supplier supplier)
        {
            try
            {
                var existingSupplier = _suppliers.FirstOrDefault(s => s.SupplierId == supplier.SupplierId);
                if (existingSupplier != null)
                {
                    existingSupplier.Name = supplier.Name;
                    existingSupplier.Address = supplier.Address;
                    existingSupplier.ContactInfo = supplier.ContactInfo;
                    // Update any other properties here
                    return Result<Supplier>.Success(existingSupplier);
                }
                else
                {
                    return Result<Supplier>.Failure("Supplier not found.");
                }
            }
            catch (Exception ex)
            {
                return Result<Supplier>.Failure($"Failed to update supplier: {ex.Message}");
            }
        }

        public async Task<Result<bool>> RemoveSupplier(int supplierId)
        {
            try
            {
                var supplierToRemove = _suppliers.FirstOrDefault(s => s.SupplierId == supplierId);
                if (supplierToRemove != null)
                {
                    _suppliers.Remove(supplierToRemove);
                    return Result<bool>.Success(true);
                }
                else
                {
                    return Result<bool>.Failure("Supplier not found.");
                }
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Failed to remove supplier: {ex.Message}");
            }
        }
    }
}
