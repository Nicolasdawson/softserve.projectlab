using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Services.Logistics;
using API.Models.Logistics.Interfaces;
using softserve.projectlabs.Shared.Utilities;
using softserve.projectlabs.Shared.Interfaces;
using softserve.projectlabs.Shared.DTOs;
using AutoMapper; // Added for AutoMapper

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierOrderController : ControllerBase
    {
        private readonly ISupplierOrderService _service;
        private readonly IMapper _mapper; // Added IMapper for DTO mapping

        public SupplierOrderController(ISupplierOrderService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ISupplierOrder>>> GetAllOrders()
        {
            var supplierOrderDtos = await _service.GetAllSupplierOrdersAsync();
            var supplierOrders = _mapper.Map<List<ISupplierOrder>>(supplierOrderDtos);
            return Ok(supplierOrders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ISupplierOrder>> GetOrderById(int id)
        {
            var supplierOrderDto = await _service.GetSupplierOrderByIdAsync(id);
            if (supplierOrderDto == null) return NotFound();
            var supplierOrder = _mapper.Map<ISupplierOrder>(supplierOrderDto);
            return Ok(supplierOrder);
        }

        [HttpPost]
        public async Task<ActionResult<ISupplierOrder>> CreateOrder([FromBody] ISupplierOrder order)
        {
            var supplierOrderDto = _mapper.Map<SupplierOrderDto>(order);
            var newOrderDto = await _service.AddSupplierOrderAsync(supplierOrderDto);
            var newOrder = _mapper.Map<ISupplierOrder>(newOrderDto);
            return CreatedAtAction(nameof(GetOrderById), new { id = newOrder.OrderId }, newOrder);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] ISupplierOrder order)
        {
            if (id != order.OrderId) return BadRequest();
            var supplierOrderDto = _mapper.Map<SupplierOrderDto>(order);
            var updated = await _service.UpdateSupplierOrderAsync(supplierOrderDto);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var deleted = await _service.DeleteSupplierOrderAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
