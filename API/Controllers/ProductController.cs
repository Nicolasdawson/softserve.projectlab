using Microsoft.AspNetCore.Mvc;
using API.Models; 
using API.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly ProductService _productService;

    // Inyección del servicio ProductService
    public ProductController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public ActionResult<Product> CreateProduct(Product product)
    {
        var result = _productService.CreateProduct(product);
        if (result.IsSuccess)
        {
            return CreatedAtAction(nameof(GetProductById), new { id = result.Data.Id }, result.Data);
        }
        return BadRequest(result.ErrorMessage); // En caso de error, devolver mensaje de error
    }

    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetProducts()
    {
        var result = _productService.GetAllProducts();
        if (result.IsSuccess)
        {
            return new JsonResult(result.Data, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }
        return BadRequest(result.ErrorMessage); // En caso de error, devolver mensaje de error
    }

    [HttpGet("{id}")]
    public ActionResult<Product> GetProductById(Guid id)
    {
        var result = _productService.GetProductById(id);
        if (result.IsSuccess)
        {
            return new JsonResult(result.Data, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }
        return NotFound(result.ErrorMessage); // En caso de no encontrar el producto, devolver un 404 con el error
    }

    [HttpGet("filtered")]
    public ActionResult<Result<PagedResult<Product>>> GetFilteredProducts(
        [FromQuery] string? name = null,
        [FromQuery] List<string>? category = null,
        [FromQuery] decimal? minPrice = null,
        [FromQuery] decimal? maxPrice = null,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var filteredProducts = _productService.GetProductsFiltered(name, category, minPrice, maxPrice, pageNumber, pageSize);

        if (filteredProducts.IsSuccess)
        {
            return new JsonResult(filteredProducts.Data, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }

        return BadRequest(filteredProducts.ErrorMessage);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateProduct(Guid id, Product updatedProduct)
    {
        var result = _productService.UpdateProduct(id, updatedProduct);
        if (result.IsSuccess)
        {
            return NoContent(); // Si se actualizó correctamente, devolver No Content
        }
        return NotFound(result.ErrorMessage); // Si no se encuentra el producto, devolver 404 con el mensaje de error
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteProduct(Guid id)
    {
        var result = _productService.DeleteProduct(id);
        if (result.IsSuccess)
        {
            return NoContent(); // Si se eliminó correctamente, devolver No Content
        }
        return NotFound(result.ErrorMessage); // Si no se encuentra el producto, devolver 404 con el mensaje de error
    }
}
