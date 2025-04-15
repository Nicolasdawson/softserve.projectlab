using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace API.Services;

public class CustomerDomainService
{
    private readonly AppDbContext _context;

    public CustomerDomainService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Customer>> GetCustomers(string? type)
    {
        var customers = await _context.Customers.ToListAsync();

        if (!string.IsNullOrEmpty(type))
        {
            customers = customers
                .Where(c => c.GetCustomerType().Equals(type, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        return customers;
    }

    public async Task<string> ExportCustomersToJson()
    {
        var customers = await _context.Customers.ToListAsync();
        var options = new JsonSerializerOptions { WriteIndented = true };
        var json = JsonSerializer.Serialize(customers, options);

        var filePath = "clientes_exportados.json";
        System.IO.File.WriteAllText(filePath, json);

        return Path.GetFullPath(filePath);
    }

    public async Task<Customer> CreateCustomer(Customer customer)
    {
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
        return customer;
    }
}