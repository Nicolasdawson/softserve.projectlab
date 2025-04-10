using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Services;

namespace API.Controllers;

[Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        /// <summary>
        /// Injects the ProductService dependency.
        /// </summary>
        /// <param name="productService">The product service.</param>
        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="product">The product to create.</param>
        /// <returns>The created product.</returns>
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            Console.WriteLine(product);

            if (product == null)
                return BadRequest("The product can't be null");

        //var categoryExists = await _context.Categories.AnyAsync(categoryExists => categoryExists.Id == product.IdCategory);
        //if (!categoryExists)
        //    return BadRequest("The selected category doesn't exist");

            // Save in DB
            var createdProduct = await _productService.CreateProductAsync(product);
            return CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct);
        }

        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <returns>A list of all products.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            var products = _productService.GetAllProducts();
            return Ok(products.Result);
        }

        /// <summary>
        /// Retrieves a product by its ID.
        /// </summary>
        /// <param name="id">The product ID.</param>
        /// <returns>The requested product if found; otherwise, NotFound.</returns>
        [HttpGet("{id}")]
        public ActionResult<Product> GetProductById(Guid id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="updatedProduct">The updated product details.</param>
        /// <returns>NoContent if successful; otherwise, NotFound.</returns>
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(Guid id, Product updatedProduct)
        {
            if (!_productService.UpdateProduct(id, updatedProduct))
            {
                return NotFound();
            }
            return Ok();
        }

        /// <summary>
        /// Deletes a product by its ID.
        /// </summary>
        /// <param name="id">The product ID.</param>
        /// <returns>NoContent if successful; otherwise, NotFound.</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(Guid id)
        {
            if (!_productService.DeleteProduct(id))
            {
                return NotFound();
            }
            return NoContent();
        }


        /// <summary>
        /// Retrieves products filtered by category.
        /// </summary>
        /// <param name="category">The category ID.</param>
        /// <returns>A list of products that belong to the specified category.</returns>
        [HttpGet("filter/{category}")] 
        public ActionResult<IEnumerable<Product>> GetProductsByCategory(Guid category)
        {
            var products = _productService.GetProductsByCategory(category);
            return Ok(products);
        }
}
