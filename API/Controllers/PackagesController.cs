using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

/// <summary>
/// API controller for managing packages.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PackagesController : ControllerBase
{
    private readonly IPackageService _packageService;

    /// <summary>
    /// Initializes a new instance of the <see cref="PackagesController"/> class.
    /// </summary>
    /// <param name="packageService">The package service.</param>
    public PackagesController(IPackageService packageService)
    {
        _packageService = packageService;
    }

    /// <summary>
    /// Creates a new package.
    /// </summary>
    /// <param name="package">The package to create.</param>
    /// <returns>The created package.</returns>
    [HttpPost]
    public async Task<ActionResult<API.Models.Package>> CreatePackage([FromBody] API.Models.Package package)
    {
        var result = await _packageService.CreatePackageAsync(package);
        return Ok(result);
    }

    /// <summary>
    /// Adds an item to a package.
    /// </summary>
    /// <param name="packageId">The ID of the package.</param>
    /// <param name="itemId">The ID of the item to add.</param>
    /// <returns>The updated package.</returns>
    [HttpPost("{packageId}/Items/{itemId}")]
    public async Task<ActionResult<Package>> AddItem([FromRoute] string packageId, [FromRoute] string itemId)
    {
        var result = await _packageService.AddItemAsync(packageId, itemId);
        return Ok(result);
    }

    /// <summary>
    /// Deletes an item from a package.
    /// </summary>
    /// <param name="packageId">The ID of the package.</param>
    /// <param name="itemId">The ID of the item to delete.</param>
    /// <returns>The updated package.</returns>
    [HttpDelete("{packageId}/Items/{itemId}")]
    public async Task<ActionResult<Package>> DeleteItem([FromRoute] string packageId, [FromRoute] string itemId)
    {
        var result = await _packageService.DeleteItemAsync(packageId, itemId);
        return Ok(result);
    }

    /// <summary>
    /// Adds a customer to a package.
    /// </summary>
    /// <param name="packageId">The ID of the package.</param>
    /// <param name="customer">The customer to add.</param>
    /// <returns>The updated package.</returns>
    [HttpPost("{packageId}/Customers")]
    public async Task<ActionResult<Package>> AddCustomer([FromRoute] string packageId, [FromBody] Customer customer)
    {
        var result = await _packageService.AddCustomerAsync(packageId, customer);
        return Ok(result);
    }
}