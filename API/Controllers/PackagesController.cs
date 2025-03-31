using API.implementations.Domain;
//using API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using API.implementations;
using System.Net;
namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PackagesController : ControllerBase
{
    private RequestProcessor _RequestProcessor = new RequestProcessor();

    //[HttpPost]
    //public async Task<ActionResult<Package>>
    //    CreatePackage([FromBody] Package package)
    //{
    //    Result<Package> r = await _RequestProcessor.CreatePackage(package);
    //    if (r.Success)
    //    {
    //        return Ok(r.Value);
    //    }
    //    else
    //    {
    //        return StatusCode(500); //InternalServerError;
    //    }
        
    //}

    //[HttpPost("{packageId}/Items/{itemId}")]
    //public async Task<ActionResult<Package>> AddItem([FromRoute] string packageId, [FromRoute] string itemId)
    //{
    //    Result<Package> r = await _RequestProcessor.AddItem(packageId, itemId);
    //    if (r.Success)
    //    {
    //        return Ok(r.Value);
    //    }
    //    else
    //    {
    //        return StatusCode(500); //InternalServerError;
    //    }
    //}

    //[HttpDelete("{packageId}/Items")]
    //public async Task<ActionResult<Package>> DeleteItem([FromRoute] string packageId, [FromRoute] string itemId)
    //{
    //    Result<Package> r = await _RequestProcessor.DeleteItem(packageId, itemId);
    //    if (r.Success)
    //    {
    //        return Ok(r.Value);
    //    }
    //    else
    //    {
    //        return StatusCode(500); //InternalServerError;
    //    }
    //}

    //[HttpPost("{packageId}/Customers")]
    //public async Task<ActionResult<Package>> AddCustomer([FromRoute] string packageId, [FromBody] Customer customer)
    //{
    //    Result<Package> r = await _RequestProcessor.AddCustomer(packageId, customer);
    //    if (r.Success)
    //    {
    //        return Ok(r.Value);
    //    }
    //    else
    //    {
    //        return StatusCode(500); //InternalServerError;
    //    }
    //}

    //TODO: we need a domain class to process this requests, use dependency injection to use those classes 
    
    // TODO 2: after implementing result classes we need to check if the domain class failed and return the correct status code
}