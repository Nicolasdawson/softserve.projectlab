using API.Abstractions;
using API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PackagesController : ControllerBase
{
    private readonly IPackageService _packageService;

    public PackagesController(IPackageService packageService)
    {
        _packageService = packageService;
    }

    [HttpGet("list")]
    public IActionResult GetPackages()
    {
        // Lógica para obtener paquetes
        return Ok(new List<string> { "Package1", "Package2" });
    }

    [HttpPost]
    public async Task<ActionResult<Package>> CreatePackage([FromBody] Package package)
    {
        var result = await _packageService.CreatePackage(package);
        return Ok(result);
    }

    [HttpPost("{packageId}/Items/{itemId}")]
    public async Task<ActionResult<Package>> AddItem([FromRoute] string packageId, [FromRoute] string itemId)
    {
        var result = await _packageService.AddItemToPackage(packageId, itemId);
        return Ok(result);
    }

    [HttpDelete("{packageId}/Items/{itemId}")]
    public async Task<ActionResult<Package>> DeleteItem([FromRoute] string packageId, [FromRoute] string itemId)
    {
        var result = await _packageService.RemoveItemFromPackage(packageId, itemId);
        return Ok(result);
    }

    [HttpPost("{packageId}/Customers")]
    public async Task<ActionResult<Package>> AddCustomer([FromRoute] string packageId, [FromBody] Customer customer)
    {
        var result = await _packageService.AddCustomerToPackage(packageId, customer);
        return Ok(result);
    }
    //TODO: we need a domain class to process this requests, use dependency injection to use those classes 
}