using API.Models.Customers;
using API.Models.IntAdmin;
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
    public async Task<ActionResult<Package>> CreatePackage([FromBody] Package package)
    {
        var result = await _packageService.CreatePackageAsync(package);
        if (result.IsSuccess)
        {
            return Ok(result.Data);
        }
        return BadRequest(result.ErrorMessage);
    }

    /// <summary>
    /// Adds an item to a package.
    /// </summary>
    /// <param name="packageId">The ID of the package.</param>
    /// <param name="item">The item to add.</param>
    /// <returns>The updated package.</returns>
    [HttpPost("{packageId}/Items")]
    public async Task<ActionResult<Package>> AddItem([FromRoute] string packageId, [FromBody] Item item)
    {
        var result = await _packageService.AddItemAsync(packageId, item);
        if (result.IsSuccess)
        {
            return Ok(result.Data);
        }
        return BadRequest(result.ErrorMessage);
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
        if (result.IsSuccess)
        {
            return Ok(result.Data);
        }
        return BadRequest(result.ErrorMessage);
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
        if (result.IsSuccess)
        {
            return Ok(result.Data);
        }
        return BadRequest(result.ErrorMessage);
    }
}

    //TODO: we need a domain class to process this requests, use dependency injection to use those classes 
    
    // TODO 2: after implementing result classes we need to check if the domain class failed and return the correct status code