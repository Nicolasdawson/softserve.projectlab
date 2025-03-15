using API.Abstractions;
using API.Implementations.Domain;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PackagesController : ControllerBase
    {
        private readonly PackagesDomain _packagesDomain;

        public PackagesController(PackagesDomain packagesDomain)
        {
            _packagesDomain = packagesDomain;
        }

        [HttpPost]
        public async Task<ActionResult<Package>> CreatePackage([FromBody] Package package)
        {
            var result = await _packagesDomain.CreatePackage(package);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }

        [HttpPost("{packageId}/Items/{sku}")]
        public async Task<ActionResult<Package>> AddItem([FromRoute] string packageId, [FromRoute] string sku)
        {
            var result = await _packagesDomain.AddItem(packageId, sku);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }

        [HttpDelete("{packageId}/Items/{sku}")]
        public async Task<ActionResult<Package>> DeleteItem([FromRoute] string packageId, [FromRoute] string sku)
        {
            var result = await _packagesDomain.DeleteItem(packageId, sku);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }

        [HttpPost("{packageId}/Customers")]
        public async Task<ActionResult<Package>> AddCustomer([FromRoute] string packageId, [FromBody] Customer customer)
        {
            var result = await _packagesDomain.AddCustomer(packageId, customer);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }
    }
}
