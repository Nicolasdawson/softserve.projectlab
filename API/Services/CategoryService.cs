using API.Abstractions;
using API.DTO;
using API.implementations.Infrastructure.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class CategoryService
{
    private readonly AppDbContext _context;

    public CategoryService(AppDbContext context) 
    {
        _context = context;
    }

    /// <summary>
    /// Retrieves a category by its ID.
    /// </summary>
    /// <param name="id">The ID of the cateogory.</param>
    /// <returns>The category with the data found it; otherwise, null.</returns>
    public async Task<ActionResponseDTO<Category>> GetCategoryById(Guid id)
    {
        var row = await _context.Categories.FindAsync(id);
        if (row == null)
        {
            return new ActionResponseDTO<Category>
            {
                WasSuccess = false,
                Message = "ERR001"
            };
        }
        return new ActionResponseDTO<Category>
        {
            WasSuccess = true,
            Result = row
        };
    }
}
