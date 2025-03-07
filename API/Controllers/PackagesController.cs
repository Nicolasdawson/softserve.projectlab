using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PackagesController : ControllerBase
{
    private readonly PackageService _packageService;

    public PackagesController(PackageService packageService)
    {
        _packageService = packageService;
    }

    [HttpPost]
    public async Task<ActionResult<Package>> CreatePackage([FromBody] Package package)
    {
        return Ok(_packageService.CreatePackage(package));
    }

    [HttpPost("{packageId}/Items/{itemId}")]
    public async Task<ActionResult<Package>> AddItem([FromRoute] string packageId, [FromRoute] string itemId)
    {
        var item = new Item { Sku = itemId, Price = 10, Discount = 0 }; // Suponiendo que el precio es 10
        return Ok(_packageService.AddItemToPackage(packageId, item));
    }

    [HttpDelete("{packageId}/Items/{itemId}")]
    public async Task<ActionResult<Package>> DeleteItem([FromRoute] string packageId, [FromRoute] string itemId)
    {
        return Ok(_packageService.RemoveItemFromPackage(packageId, itemId));
    }

    [HttpPost("{packageId}/Customers")]
    public async Task<ActionResult<Package>> AddCustomer([FromRoute] string packageId, [FromBody] Customer customer)
    {
        return Ok(_packageService.AddCustomerToPackage(packageId, customer));
    }
}
