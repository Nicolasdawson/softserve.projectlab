using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpPost("Get/{type}")]
        public IEnumerable<string> GetProduct(string type, [FromBody] string? value)
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{type}/{id}")]
        public string GetProduct(string type, int id)
        {
            return "value";
        }

        [HttpPost("{type}")]
        public void RegisterProduct(string type, [FromBody] string value)
        {

        }

        [HttpPut("{type}/{id}")]
        public void UpdateProduct(string type, int id, [FromBody] string value)
        {

        }

        [HttpDelete("{type}/{id}")]
        public void DeleteProduct(string type, int id)
        {

        }

    }
}
