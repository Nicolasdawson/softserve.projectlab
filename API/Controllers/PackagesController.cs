using API.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using API.Services;

namespace API.Controllers
{
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
            var result = await _packageService.CreatePackage(package);
            return Ok(result);
        }

        [HttpPost("{packageId}/Items/{itemId}")]
        public async Task<ActionResult<Package>> AddItem(string packageId, string itemSku)
        {
            var result = await _packageService.AddItemToPackage(packageId, itemSku);
            if (result == null) return NotFound("Package not found");
            return Ok(result);
        }

        [HttpDelete("{packageId}/Items/{itemId}")]
        public async Task<ActionResult> DeleteItem(string packageId, string itemSku)
        {
            var success = await _packageService.DeleteItemFromPackage(packageId, itemSku);
            if (!success) return NotFound("Package or Item not found");
            return NoContent();
        }

        [HttpPost("{packageId}/Customers")]
        public async Task<ActionResult<Package>> AddCustomer(string packageId, [FromBody] Customer customer)
        {
            var result = await _packageService.AddCustomerToPackage(packageId, customer);
            if (result == null) return NotFound("Package not found");
            return Ok(result);
        }
    }
    //TODONE: we need a domain class to process this requests, use dependency injection to use those classes 
}