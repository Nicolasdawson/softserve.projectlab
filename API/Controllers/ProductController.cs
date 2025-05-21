using Microsoft.AspNetCore.Mvc;
using API.Models;
using API.Services;
using API.DTO;
using API.Helpers;
using API.implementations.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IProductService _productService;
        private readonly IProductImageService _productImageService;
        private readonly AppDbContext _context;
        private readonly IFileStorage _fileStorage;
        
        /// <summary>
        /// Injects the dependencies.
        /// </summary>
        /// <param name="productService">The product service.</param>
        public ProductController(
            IConfiguration configuration, 
            IProductService productService, 
            IProductImageService productImageService, 
            IFileStorage fileStorage, 
            AppDbContext context )
        {
            _configuration = configuration;
            _productService = productService;
            _productImageService = productImageService;
            _context = context; 
            _fileStorage = fileStorage;
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="product">The product to create.</param>
        /// <returns>The created product.</returns>
        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct([FromForm] ProductDTO product)
        {
            if (product == null)
                return BadRequest("The product can't be null");

            var categoryExists = await _context.Categories.AnyAsync(categoryExists => categoryExists.Id == product.IdCategory);
            if (!categoryExists)
                return BadRequest("The selected category doesn't exist");


            Product prod = new Product
            {
                Id = Guid.NewGuid(),
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Weight = product.Weight,
                Height = product.Height,
                Width = product.Width,
                Length = product.Length,
                IdCategory = product.IdCategory,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            //Store the Images in folder
            if (product.Images != null) {            
                var images = new List<ProductImage>();
                foreach (var image in product.Images)
                {
                    var fileName = await _fileStorage.SaveLocalFileAsync(image);                
                    var imagePath = _configuration["urlBackEnd"] + "/Images/" + fileName;

                    images.Add(new ProductImage {
                        Id = Guid.NewGuid(),
                        ImageUrl = imagePath,
                        IdProduct = prod.Id,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,

                    });
                }
                await _productImageService.CreateProductImageAsync(images);
            }

            await _productService.CreateProductAsync(prod);
            return StatusCode(StatusCodes.Status201Created); // CreatedAtAction(nameof(GetProductById), new { id = createdProduct.Id }, createdProduct);
        }

        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <returns>A list of all products.</returns>
        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<ProductWithImagesDTO>>> GetProducts(
            [FromQuery][Range(0, Int32.MaxValue)] int pageNumber = 1, 
            [FromQuery][Range(0, Int32.MaxValue)] int pageSize = 10)
        {
            if (pageNumber <= 0 || pageSize <= 0)
                return BadRequest("Page number and page size must be greater than zero.");

            try
            {
                var products = await _productService.GetAllProductsPaged(pageNumber, pageSize);
                return Ok(products);
            }
            catch (Exception ex)
            {
                //Is needed a Logger for printing the error ex
                return StatusCode(500, $"Error retrieving products.");
            }
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
