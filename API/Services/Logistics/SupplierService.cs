using API.Implementations.Domain;
using API.Models;
using Logistics.Models;

namespace API.Services.Logistics
{
    public class SupplierService : ISupplierService
    {
        private readonly SupplierDomain _supplierDomain;

        /// <summary>
        /// Initializes a new instance of the <see cref="SupplierService"/> class.
        /// </summary>
        /// <param name="supplierDomain">The supplier domain.</param>
        public SupplierService(SupplierDomain supplierDomain)
        {
            _supplierDomain = supplierDomain;
        }

        /// <summary>
        /// Creates a new supplier asynchronously.
        /// </summary>
        /// <param name="supplier">The supplier to create.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the result of the creation operation.</returns>
        public async Task<Result<Supplier>> CreateSupplierAsync(Supplier supplier)
        {
            return await _supplierDomain.CreateSupplier(supplier);
        }

        /// <summary>
        /// Gets a supplier by its identifier asynchronously.
        /// </summary>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the result of the retrieval operation.</returns>
        public async Task<Result<Supplier>> GetSupplierByIdAsync(int supplierId)
        {
            return await _supplierDomain.GetSupplierById(supplierId);
        }

        /// <summary>
        /// Gets all suppliers asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the result of the retrieval operation.</returns>
        public async Task<Result<List<Supplier>>> GetAllSuppliersAsync()
        {
            return await _supplierDomain.GetAllSuppliers();
        }

        /// <summary>
        /// Updates a supplier asynchronously.
        /// </summary>
        /// <param name="supplier">The supplier to update.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the result of the update operation.</returns>
        public async Task<Result<Supplier>> UpdateSupplierAsync(Supplier supplier)
        {
            return await _supplierDomain.UpdateSupplier(supplier);
        }

        /// <summary>
        /// Deletes a supplier by its identifier asynchronously.
        /// </summary>
        /// <param name="supplierId">The supplier identifier.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the result of the deletion operation.</returns>
        public async Task<Result<bool>> DeleteSupplierAsync(int supplierId)
        {
            return await _supplierDomain.RemoveSupplier(supplierId);
        }
    }
}
