using API.implementations.Infrastructure.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class ProductImageService
    {
        private readonly AppDbContext _context;

        public ProductImageService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Boolean> CreateProductImageAsync(List<ProductImage> images)
        {
            try
            {
                _context.ProductImages.AddRange(images);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error happen during createProductImageAsync.", ex);
            }

            return true;
        }

        public async Task<Boolean> GetProductImagesByProductId(Guid id)
        {
            var images = await _context.ProductImages
                .Where(p => p.IdProduct == id)
                .ToListAsync();
            foreach (var item in images)
            {
                Console.WriteLine(item);                
            }
            return true; 
        }
    }
}
