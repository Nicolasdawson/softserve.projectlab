using API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PackagesController : ControllerBase
{
    private RequestProcessor _RequestProcessor = new RequestProcessor();

    [HttpPost]
    public async Task<ActionResult<Package>> CreatePackage([FromBody] Package package)
    {
        Package p = _RequestProcessor.CreatePackage(package);
        Console.WriteLine("XD");
        return Ok(p);
    }

    [HttpPost("{packageId}/Items/{itemId}")]
    public async Task<ActionResult<Package>> AddItem([FromRoute] string packageId, [FromRoute] string itemId)
    {
        Package p = _RequestProcessor.AddItem(packageId, itemId);
        return Ok(p);
    }

    [HttpDelete("{packageId}/Items")]
    public async Task<ActionResult<Package>> DeleteItem([FromRoute] string packageId, [FromRoute] string itemId)
    {
        Package p = _RequestProcessor.DeleteItem(packageId, itemId);
        return Ok(p);
    }

    [HttpPost("{packageId}/Customers")]
    public async Task<ActionResult<Package>> AddCustomer([FromRoute] string packageId, [FromBody] Customer customer)
    {
        Package p = _RequestProcessor.AddCustomer(packageId, customer);
        return Ok(p);
    }

    //TODO: we need a domain class to process this requests, use dependency injection to use those classes 
}