using API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using API.implementations.Domain;
using API.implementations;

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
        Result<Package> result = await _packageService.CreatePackage(package);

        if (result.Success)
        {
            return Ok(result.Value);
        }
        else
        {
            return StatusCode(500); //InternalServerError;
        }
    }

    [HttpPost("{packageId}/Items/{itemId}")]
    public async Task<ActionResult<Package>> AddItem([FromRoute] string packageId, [FromRoute] string itemId)
    {
        Result<Package> result = await _packageService.AddItem(packageId, itemId);
        if (result.Success)
        {
            return Ok(result.Value);
        }
        else
        {
            return StatusCode(500); //InternalServerError;
        }
    }

    [HttpDelete("{packageId}/Items")]
    public async Task<ActionResult<Package>> DeleteItem([FromRoute] string packageId, [FromRoute] string itemId)
    {
        Result<Package> result = await _packageService.DeleteItem(packageId, itemId);
        if (result.Success)
        {
            return Ok(result.Value);
        }
        else
        {
            return StatusCode(500); //InternalServerError;
        }
    }

    [HttpPost("{packageId}/Customers")]
    public async Task<ActionResult<Package>> AddCustomer([FromRoute] string packageId, [FromBody] Customer customer)
    {
        Result<Package> result = await _packageService.AddCustomer(packageId, customer);
        if (result.Success)
        {
            return Ok(result.Value);
        }
        else
        {
            return StatusCode(500); //InternalServerError;
        }
    }

    //TODO: we need a domain class to process this requests, use dependency injection to use those classes 
    // TODO 2: after implementing result classes we need to check if the domain class failed and return the correct status code
}