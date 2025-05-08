using API.Data.Entities;
using API.Data.Repositories.LogisticsRepositories.Interfaces;
using API.Models;
using API.Models.IntAdmin;
using Logistics.Models;
using softserve.projectlabs.Shared.DTOs;
using softserve.projectlabs.Shared.Utilities;

namespace API.Implementations.Domain
{
    
    public class SupplierDomain
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierDomain(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<Result<Supplier>> CreateSupplier(Supplier supplier)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(supplier.GetSupplierData().Name))
                {
                    return Result<Supplier>.Failure("Supplier name cannot be empty.");
                }

                var supplierEntity = new SupplierEntity
                {
                    SupplierName = supplier.GetSupplierData().Name,
                    SupplierAddress = supplier.GetSupplierData().Address,
                    SupplierContactNumber = supplier.GetSupplierData().ContactNumber,
                    SupplierContactEmail = supplier.GetSupplierData().ContactEmail,
                    IsDeleted = false
                };

                await _supplierRepository.AddAsync(supplierEntity);

                return Result<Supplier>.Success(supplier);
            }
            catch (Exception ex)
            {
                return Result<Supplier>.Failure($"Failed to create supplier: {ex.Message}");
            }
        }

        public async Task<Result<Supplier>> GetSupplierByIdAsync(int supplierId)
        {
            try
            {
                var supplierEntity = await _supplierRepository.GetByIdAsync(supplierId);
                if (supplierEntity == null)
                    return Result<Supplier>.Failure("Supplier not found.");

                var supplierDto = new SupplierDto
                {
                    SupplierId = supplierEntity.SupplierId,
                    Name = supplierEntity.SupplierName,
                    Address = supplierEntity.SupplierAddress,
                    ContactNumber = supplierEntity.SupplierContactNumber,
                    ContactEmail = supplierEntity.SupplierContactEmail
                };

                var supplier = new Supplier(supplierDto);
                return Result<Supplier>.Success(supplier);
            }
            catch (Exception ex)
            {
                return Result<Supplier>.Failure($"Failed to retrieve supplier: {ex.Message}");
            }
        }

        public async Task<Result<List<Supplier>>> GetAllSuppliersAsync()
        {
            try
            {
                var supplierEntities = await _supplierRepository.GetAllAsync();
                var suppliers = supplierEntities.Select(entity =>
                {
                    var supplierDto = new SupplierDto
                    {
                        SupplierId = entity.SupplierId,
                        Name = entity.SupplierName,
                        Address = entity.SupplierAddress,
                        ContactNumber = entity.SupplierContactNumber,
                        ContactEmail = entity.SupplierContactEmail
                    };

                    return new Supplier(supplierDto);
                }).ToList();

                return Result<List<Supplier>>.Success(suppliers);
            }
            catch (Exception ex)
            {
                return Result<List<Supplier>>.Failure($"Failed to retrieve suppliers: {ex.Message}");
            }
        }

        public async Task<Result<Supplier>> UpdateSupplier(Supplier existingSupplier, Supplier updatedSupplier)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(updatedSupplier.GetSupplierData().Name))
                {
                    return Result<Supplier>.Failure("Supplier name cannot be empty.");
                }

                var supplierEntity = new SupplierEntity
                {
                    SupplierId = existingSupplier.GetSupplierData().SupplierId,
                    SupplierName = updatedSupplier.GetSupplierData().Name,
                    SupplierAddress = updatedSupplier.GetSupplierData().Address,
                    SupplierContactNumber = updatedSupplier.GetSupplierData().ContactNumber,
                    SupplierContactEmail = updatedSupplier.GetSupplierData().ContactEmail,
                    IsDeleted = false
                };

                await _supplierRepository.UpdateAsync(supplierEntity);

                return Result<Supplier>.Success(updatedSupplier);
            }
            catch (Exception ex)
            {
                return Result<Supplier>.Failure($"Failed to update supplier: {ex.Message}");
            }
        }

        public async Task<Result<bool>> RemoveSupplierAsync(int supplierId)
        {
            try
            {
                await _supplierRepository.DeleteAsync(supplierId);
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
