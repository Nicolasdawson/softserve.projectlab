using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Services;
using API.DTO.DeliveryAddress;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DeliveryAddressController : ControllerBase
{
    private readonly DeliveryAddressService _service;

    public DeliveryAddressController(DeliveryAddressService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DeliveryAddressRequest request)
    {
        var address = await _service.CreateFromRequestAsync(request);
        var response = await _service.MapToResponseAsync(address);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }



    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var address = await _service.GetByIdAsync(id);
        if (address == null) return NotFound();

        var response = await _service.MapToResponseAsync(address);
        return Ok(response);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _service.DeleteAsync(id);
        if (!success)
            return NotFound();

        return NoContent();
    }

    [HttpGet("by-customer/{customerId}")]
    public async Task<IActionResult> GetByCustomerViaOrders(int customerId)
    {
        var addresses = await _service.GetByCustomerIdThroughOrdersAsync(customerId);

        var responses = new List<DeliveryAddressResponse>();
        foreach (var address in addresses)
        {
            responses.Add(await _service.MapToResponseAsync(address));
        }

        return Ok(responses);
    }

}
