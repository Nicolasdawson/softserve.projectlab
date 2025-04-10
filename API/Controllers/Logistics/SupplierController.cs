using API.Services.Logistics;
using Logistics.Models;
using Microsoft.AspNetCore.Mvc;
using softserve.projectlabs.Shared.Utilities;
using softserve.projectlabs.Shared.Interfaces;
using softserve.projectlabs.Shared.DTOs;
using AutoMapper; // Add this for mapping

namespace API.Controllers.Logistics
{
    [Route("api/suppliers")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;
        private readonly IMapper _mapper; // Add IMapper for DTO mapping

        public SupplierController(ISupplierService supplierService, IMapper mapper)
        {
            _supplierService = supplierService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSupplier([FromBody] Supplier supplier)
        {
            var supplierDto = _mapper.Map<SupplierDto>(supplier); // Map Supplier to SupplierDto
            var result = await _supplierService.CreateSupplierAsync(supplierDto);
            if (!result.IsSuccess)
                return BadRequest(result.ErrorMessage);

            return CreatedAtAction(nameof(GetSupplierById), new { supplierId = result.Data.SupplierId }, result.Data);
        }

        [HttpGet("{supplierId}")]
        public async Task<IActionResult> GetSupplierById(int supplierId)
        {
            var result = await _supplierService.GetSupplierByIdAsync(supplierId);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSuppliers()
        {
            var result = await _supplierService.GetAllSuppliersAsync();
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        [HttpPut("{supplierId}")]
        public async Task<IActionResult> UpdateSupplier(int supplierId, [FromBody] Supplier supplier)
        {
            var supplierDto = _mapper.Map<SupplierDto>(supplier); // Map Supplier to SupplierDto
            supplierDto.SupplierId = supplierId; // Ensure the ID is set correctly
            var result = await _supplierService.UpdateSupplierAsync(supplierDto);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        [HttpDelete("{supplierId}")]
        public async Task<IActionResult> DeleteSupplier(int supplierId)
        {
            var result = await _supplierService.DeleteSupplierAsync(supplierId);
            if (result.IsNoContent)
            {
                return NoContent(); // Returns HTTP 204 No Content
            }
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }
    }
}

