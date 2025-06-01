using API.implementations.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using API.Models;
using API.DTO;

namespace API.Services;

public class ProductService : IProductService
{
    private readonly AppDbContext _context;
    private readonly IStockReservationService  _stockReservationService;

    public ProductService(AppDbContext context, IStockReservationService stockReservationService)
    {
        _context = context;
        _stockReservationService = stockReservationService;
    }


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
        return await Task.FromResult(product);
    }

    public async Task<IEnumerable<ProductWithImagesDTO>> GetAllProductsPaged(int pageNumber, int pageSize)
    {
        return await _context.Products
            .Where(p => p.IsActive)
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
                CategoryName = p.Category!.Name
            })
            .ToListAsync();
    }

    public async Task<ProductDetailWithImages?> GetProductDetailById(Guid id)
    {
        return await _context.Products
            .Where(p => p.IsActive)
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
                categoryName = p.Category!.Name,
                ImageUrls = p.Images.Select(img => img.ImageUrl).ToList()
            })
            .FirstOrDefaultAsync();
    }

    /*public async Task<bool> DeleteProduct(Guid id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
            return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return true;
    }*/

    public async Task<bool> DeleteProduct(Guid id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return false;

        product.IsActive = false;
        await _context.SaveChangesAsync();

        return true;
    }



    public async Task<bool> UpdateProduct(Guid id, Product updatedProduct)
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
    public IEnumerable<Product> GetProductsByCategory(Guid category)
    {
        var products = _context.Products.Where(p => p.IdCategory == category && p.IsActive);
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