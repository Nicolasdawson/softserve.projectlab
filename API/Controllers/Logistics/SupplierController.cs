using API.Services.Logistics;
using Logistics.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Logistics
{
    [Route("api/suppliers")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        /// <summary>
        /// Creates a new supplier.
        /// </summary>
        /// <param name="supplier">The supplier to create.</param>
        /// <returns>An IActionResult containing the result of the creation operation.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateSupplier([FromBody] Supplier supplier)
        {
            var result = await _supplierService.CreateSupplierAsync(supplier);
            if (!result.IsSuccess)
                return BadRequest(result.ErrorMessage);

            return CreatedAtAction(nameof(GetSupplierById), new { supplierId = supplier.SupplierId }, supplier);
        }

        /// <summary>
        /// Gets a supplier by its ID.
        /// </summary>
        /// <param name="supplierId">The ID of the supplier to retrieve.</param>
        /// <returns>An IActionResult containing the supplier data or an error message.</returns>
        [HttpGet("{supplierId}")]
        public async Task<IActionResult> GetSupplierById(int supplierId)
        {
            var result = await _supplierService.GetSupplierByIdAsync(supplierId);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        /// <summary>
        /// Gets all suppliers.
        /// </summary>
        /// <returns>An IActionResult containing the list of all suppliers or an error message.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllSuppliers()
        {
            var result = await _supplierService.GetAllSuppliersAsync();
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        /// <summary>
        /// Updates an existing supplier.
        /// </summary>
        /// <param name="supplierId">The ID of the supplier to update.</param>
        /// <param name="supplier">The updated supplier data.</param>
        /// <returns>An IActionResult containing the result of the update operation.</returns>
        [HttpPut("{supplierId}")]
        public async Task<IActionResult> UpdateSupplier(int supplierId, [FromBody] Supplier supplier)
        {
            var result = await _supplierService.UpdateSupplierAsync(supplier);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        /// <summary>
        /// Deletes a supplier by its ID.
        /// </summary>
        /// <param name="supplierId">The ID of the supplier to delete.</param>
        /// <returns>An IActionResult containing the result of the deletion operation.</returns>
        [HttpDelete("{supplierId}")]
        public async Task<IActionResult> DeleteSupplier(int supplierId)
        {
            var result = await _supplierService.DeleteSupplierAsync(supplierId);
            //return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
            if (result.IsNoContent)
            {
                return NoContent();  // Returns HTTP 204 No Content
            }
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }
    }
}
