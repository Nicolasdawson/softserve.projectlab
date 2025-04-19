using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly CustomerDomainService _service;

    public CustomerController(CustomerDomainService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetCustomers([FromQuery] string? type)
    {
        var customers = await _service.GetCustomers(type);
        return Ok(customers);
    }

    [HttpGet("export")]
    public async Task<IActionResult> ExportCustomersToJson()
    {
        var filePath = await _service.ExportCustomersToJson();
        return Ok($"Archivo generado en: {filePath}");
    }

    [HttpPost]
    public async Task<ActionResult<Customer>> CreateCustomer([FromBody] Customer customer)
    {
        var created = await _service.CreateCustomer(customer);
        return CreatedAtAction(nameof(CreateCustomer), new { id = created.Id }, created);
    }
}