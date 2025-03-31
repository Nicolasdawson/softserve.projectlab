using API.implementations.Domain;
using API.Data.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // PRODUCT CONTROLLER

        private readonly IProductProcessor _productProcessor;
        public ProductsController(IProductProcessor productProcessor)
        {
            _productProcessor = productProcessor;
        }

        [HttpPost("Get/{type}")]
        public List<Product> GetProduct(string type, [FromBody] string? value)
        {
            return _productProcessor.GetAllProducts(true);
        }

        [HttpGet("{id}")]
        public Product? GetProduct(int id)
        {
            return _productProcessor.GetProductByID(id);
        }

        [HttpPost("{type}")]
        public bool AddProduct(string type, [FromBody] Product obj)
        {
            return _productProcessor.AddProduct(type, obj);
        }

        [HttpPut("{id}")]
        public bool UpdateProduct(int id, [FromBody] Product value)
        {
            return _productProcessor.UpdateProduct(id, value);
        }

        [HttpDelete("{id}")]
        public bool DeleteProduct(int id)
        {
            return _productProcessor.DeleteProduct(id);
        }

        // PRODUCT ATTRIBUTE CONTROLLER

        [HttpPost("{id_product}")]
        public bool RegisterAttribute(int id_product, [FromBody] API.Data.Models.Attribute value)
        {
            return _productProcessor.AddAttribute(id_product, value);
        }

        [HttpPatch("{id_attribute}")]
        public bool UpdateAttribute(int id_attribute, [FromBody] API.Data.Models.Attribute value)
        {
            return _productProcessor.UpdateAttribute(id_attribute, value);
        }

        [HttpDelete("{id_attribute}")]
        public bool DeleteAttribute(int id_attribute)
        {
            return _productProcessor.DeleteAttribute(id_attribute);
        }

    }
}
