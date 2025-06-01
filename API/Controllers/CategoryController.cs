using Microsoft.AspNetCore.Mvc;
using API.implementations.Infrastructure.Data;
using API.Models;
using API.Services;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : GenericController<Category>
{
    private readonly AppDbContext _context;
    private readonly CategoryService _categoryService;
    public CategoryController(
        CategoryService categoryService,
        IGenericService<Category> genericService, 
        AppDbContext context) : base(genericService)
    {
        _context = context;
        _categoryService = categoryService; 
    }

    [HttpGet("{id}")]
    public virtual async Task<IActionResult> GetAsync(Guid id)
    {
        var action = await _categoryService.GetCategoryById(id);
        if (action.WasSuccess)
        {
            return Ok(action.Result);
        }
        return NotFound();
    }
}