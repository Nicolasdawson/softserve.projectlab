using API.implementations.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using API.Models;
using API.DTO;

namespace API.Services;

/// <summary>
/// Service for managing products.
/// </summary>
public class ProductService : IProductService
{
    private readonly AppDbContext _context; 

    /// <summary>
    /// Initializes a new instance of the ProductService class.
    /// </summary>
    /// <param name="context">The application database context.</param>
    public ProductService(AppDbContext context) 
    {
        _context = context;
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
        return await _context.Products
            .Include(p => p.Images)
            .Include(p => p.Category)
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
                ImageUrls = p.Images.Select(img => img.ImageUrl).ToList(),
                CategoryName = p.Category.Name
            })
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves a product by its ID, including image URLs.
    /// </summary>
    /// <param name="id">The ID of the product.</param>
    /// <returns>The product with image URLs if found; otherwise, null.</returns>
    public async Task<ProductDetailWithImages?> GetProductDetailById(Guid id)
    {
        return await _context.Products
            .Include(p => p.Images)
            .Include(p => p.Category)
            .Where(p => p.Id == id)
            .Select(p => new ProductDetailWithImages
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Weight = p.Weight,
                Height = p.Height,
                Width = p.Width,
                Length = p.Length,
                Stock = p.Stock,
                categoryName = p.Category.Name,
                ImageUrls = p.Images.Select(img => img.ImageUrl).ToList()
            })
            .FirstOrDefaultAsync();
    }

    /// <summary>
    /// Deletes a product by its ID.
    /// </summary>
    /// <param name="id">The ID of the product to delete.</param>
    /// <returns>True if the product was deleted; otherwise, false.</returns>
    public async Task<bool> DeleteProduct(Guid id)
    {
        /*
        Product product = await GetProductById(id);
        if (product != null)
        {
            _context.Products.Remove(product);
            return true;
        }
         */
        return false;
    }

    /// <summary>
    /// Updates an existing product.
    /// </summary>
    /// <param name="id">The ID of the product to update.</param>
    /// <param name="updatedProduct">The updated product information.</param>
    /// <returns>True if the product was updated; otherwise, false.</returns>
    public async Task<bool> UpdateProduct(Guid id, Product updatedProduct)
    {
        /*
        var existingProduct = await GetProductById(id);
        if (existingProduct != null)
        {
            existingProduct.Name = updatedProduct.Name;
            existingProduct.IdCategory = updatedProduct.IdCategory;
            existingProduct.Description = updatedProduct.Description;
            //existingProduct.ImageUrl = updatedProduct.ImageUrl;
            existingProduct.Price = updatedProduct.Price;
            return true;
        }
         */
        return false;
    }

    /// <summary>
    /// Retrieves products by category.
    /// </summary>
    /// <param name="category">The category ID.</param>
    /// <returns>A collection of products in the specified category.</returns>
    public IEnumerable<Product> GetProductsByCategory(Guid category)
    {
        var products = _context.Products.Where(p => p.IdCategory == category);
        return products;
    }

    public async Task<ActionResponseDTO<int>> GetTotalRecordsAsync(PaginationDTO pagination)
    {
        var queryable = _context.Products.AsQueryable();

        if (!string.IsNullOrWhiteSpace(pagination.Filter))
        {
            queryable = queryable.Where(x => x.Name.ToLower().Contains(pagination.Filter.ToLower()));
        }

        double count = await queryable.CountAsync();
        return new ActionResponseDTO<int>
        {
            WasSuccess = true,
            Result = (int)count
        };
    }
}