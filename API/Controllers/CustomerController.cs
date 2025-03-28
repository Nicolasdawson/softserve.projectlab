using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using API.Data;
using System.Text.Json;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly AppDbContext _context;

    public CustomerController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetCustomers([FromQuery] string? type)
    {
        var customers = await _context.Customers.ToListAsync();

        // Filtro por tipo (si se proporciona)
        if (!string.IsNullOrEmpty(type))
        {
            customers = customers
                .Where(c => c.GetCustomerType().Equals(type, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        return Ok(customers);
    }

    [HttpGet("export")]
    public async Task<IActionResult> ExportCustomersToJson()
    {
        var customers = await _context.Customers.ToListAsync();

        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(customers, options);

        var filePath = "clientes_exportados.json";
        System.IO.File.WriteAllText(filePath, json);

        return Ok($"Archivo generado en: {Path.GetFullPath(filePath)}");
    }

    [HttpPost]
    public async Task<ActionResult<Customer>> CreateCustomer([FromBody] Customer customer)
    {
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(CreateCustomer), new { id = customer.Id }, customer);
    }
}