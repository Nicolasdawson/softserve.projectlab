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

    public CategoryController(
        IGenericService<Category> genericService, 
        AppDbContext context) : base(genericService)
    {
        _context = context;
    }

   
}