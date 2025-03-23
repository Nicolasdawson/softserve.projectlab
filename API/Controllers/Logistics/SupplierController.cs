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

        [HttpPost]
        public async Task<IActionResult> CreateSupplier([FromBody] Supplier supplier)
        {
            var result = await _supplierService.CreateSupplierAsync(supplier);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSupplierById(int id)
        {
            var result = await _supplierService.GetSupplierByIdAsync(id);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSuppliers()
        {
            var result = await _supplierService.GetAllSuppliersAsync();
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSupplier([FromBody] Supplier supplier)
        {
            var result = await _supplierService.UpdateSupplierAsync(supplier);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            var result = await _supplierService.DeleteSupplierAsync(id);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }
    }
}
