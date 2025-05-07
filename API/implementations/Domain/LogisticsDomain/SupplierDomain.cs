using API.Models;
using API.Models.IntAdmin;
using Logistics.Models;
using softserve.projectlabs.Shared.Utilities;

namespace API.Implementations.Domain
{
    
    public class SupplierDomain
    {
        public Result<Supplier> CreateSupplier(Supplier supplier)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(supplier.GetSupplierData().Name))
                {
                    return Result<Supplier>.Failure("Supplier name cannot be empty.");
                }

                return Result<Supplier>.Success(supplier);
            }
            catch (Exception ex)
            {
                return Result<Supplier>.Failure($"Failed to create supplier: {ex.Message}");
            }
        }

        public Result<Supplier> GetSupplierById(int supplierId, List<Supplier> suppliers)
        {
            try
            {
                var supplier = suppliers.FirstOrDefault(s => s.GetSupplierData().SupplierId == supplierId);

                return supplier != null
                    ? Result<Supplier>.Success(supplier)
                    : Result<Supplier>.Failure("Supplier not found.");
            }
            catch (Exception ex)
            {
                return Result<Supplier>.Failure($"Failed to retrieve supplier: {ex.Message}");
            }
        }

        public Result<List<Supplier>> GetAllSuppliers(List<Supplier> suppliers)
        {
            try
            {
                return suppliers.Any()
                    ? Result<List<Supplier>>.Success(suppliers)
                    : Result<List<Supplier>>.Failure("No suppliers found.");
            }
            catch (Exception ex)
            {
                return Result<List<Supplier>>.Failure($"Failed to retrieve suppliers: {ex.Message}");
            }
        }

        public Result<Supplier> UpdateSupplier(Supplier existingSupplier, Supplier updatedSupplier)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(updatedSupplier.GetSupplierData().Name))
                {
                    return Result<Supplier>.Failure("Supplier name cannot be empty.");
                }

                var existingSupplierData = existingSupplier.GetSupplierData();
                var updatedSupplierData = updatedSupplier.GetSupplierData();

                existingSupplierData.Name = updatedSupplierData.Name;
                existingSupplierData.ContactNumber = updatedSupplierData.ContactNumber;
                existingSupplierData.ContactEmail = updatedSupplierData.ContactEmail;
                existingSupplierData.Address = updatedSupplierData.Address;

                return Result<Supplier>.Success(existingSupplier);
            }
            catch (Exception ex)
            {
                return Result<Supplier>.Failure($"Failed to update supplier: {ex.Message}");
            }
        }

        public Result<bool> RemoveSupplier(Supplier supplier)
        {
            try
            {
                if (supplier == null)
                {
                    return Result<bool>.Failure("Supplier not found.");
                }

                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure($"Failed to remove supplier: {ex.Message}");
            }
        }

        public Result<bool> ValidateAddItemToSupplier(Supplier supplier, Item item, int quantity)
        {
            if (supplier == null)
                return Result<bool>.Failure("Supplier does not exist.");

            if (item == null)
                return Result<bool>.Failure("Item does not exist.");

            if (quantity <= 0)
                return Result<bool>.Failure("Quantity must be greater than zero.");

            return Result<bool>.Success(true);
        }
    }
}
