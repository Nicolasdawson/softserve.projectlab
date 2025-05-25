using API.implementations.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using API.Models;
using API.DTO;

namespace API.Services;

    /// <summary>
    /// Service for managing products.
    /// </summary>
    public class ProductService
    {
        private readonly AppDbContext _context; 
        private readonly StockReservationService _stockReservationService; 

        /// <summary>
        /// Initializes a new instance of the ProductService class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        public ProductService(AppDbContext context, StockReservationService stockReservationService) 
        {
            _context = context;
            _stockReservationService = stockReservationService;
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="product">The product to create.</param>
        /// <returns>The created product.</returns>
        public async Task<Product> CreateProductAsync(Product product)
        {
            try
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                await _stockReservationService.SetStockAsync(product.Id, product.Stock);


            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error happen during createProductAsync.", ex);
            }
            // Log para verificar si se agrega
            Console.WriteLine($"Producto creado: {product.Name} con ID {product.Id}");
            return await Task.FromResult(product);
        }

        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <returns>A collection of all products.</returns>
        public async Task<IEnumerable<ProductWithImagesDTO>> GetAllProductsPaged(int pageNumber, int pageSize)
        {
            try
            {
                var products = await _context.Products
                    .Include(p => p.Images)
                    .OrderBy(p => p.Id)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Select(p => new ProductWithImagesDTO
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        Price = p.Price,
                        Stock = p.Stock,
                        ImageUrls = p.Images.Select(img => img.ImageUrl).ToList()
                    })
                    .ToListAsync();

                Console.WriteLine($"Cantidad de productos en memoria: {products.Count}");

                return products; // No es necesario usar Task.FromResult
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener los productos: {ex.Message}");
                return new List<ProductWithImagesDTO>(); // Retornar una lista vac�a en caso de error
            }
        }

        /// <summary>
        /// Retrieves a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product.</param>
        /// <returns>The product if found; otherwise, null.</returns>
        public async Task<Product?> GetProductByIdAsync(Guid id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        /// <summary>
        /// Deletes a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>True if the product was deleted; otherwise, false.</returns>
        public async Task<bool> DeleteProduct(Guid id)
        {
            var product = await GetProductByIdAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

    /// <summary>
    /// Updates an existing product.
    /// </summary>
    /// <param name="id">The ID of the product to update.</param>
    /// <param name="updatedProduct">The updated product information.</param>
    /// <returns>True if the product was updated; otherwise, false.</returns>
    /*public bool UpdateProduct(Guid id, Product updatedProduct)
    {
        var existingProduct = GetProductById(id);
        if (existingProduct != null)
        {
            existingProduct.Name = updatedProduct.Name;
            existingProduct.IdCategory = updatedProduct.IdCategory;
            existingProduct.Description = updatedProduct.Description;
            //existingProduct.ImageUrl = updatedProduct.ImageUrl;
            existingProduct.Price = updatedProduct.Price;
            existingProduct.Stock = updatedProduct.Stock;
            return true;
        }
        return false;
    }*/
        
    public async Task<bool> UpdateProductAsync(Guid id, Product updatedProduct)
    {
        var existingProduct = await _context.Products.FindAsync(id);
        if (existingProduct == null)
            return false;

        bool stockChanged = existingProduct.Stock != updatedProduct.Stock;

        existingProduct.Name = updatedProduct.Name;
        existingProduct.Description = updatedProduct.Description;
        existingProduct.Price = updatedProduct.Price;
        existingProduct.Stock = updatedProduct.Stock;
        existingProduct.IdCategory = updatedProduct.IdCategory;

        await _context.SaveChangesAsync();

        if (stockChanged)
        {
            await _stockReservationService.SetStockAsync(existingProduct.Id, updatedProduct.Stock);
        }

        return true;
    }


        /// <summary>
        /// Retrieves products by category.
        /// </summary>
        /// <param name="category">The category ID.</param>
        /// <returns>A collection of products in the specified category.</returns>
        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(Guid category)
        {
            return await _context.Products.Where(p => p.IdCategory == category).ToListAsync();
        }

    }
