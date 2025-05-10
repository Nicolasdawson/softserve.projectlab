using API.Data.Models;
using API.Data.Models.DTOs.Category;
using API.Repository;
using API.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
           this.categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categories = await categoryRepository.GetCategories();

            if (categories.Any())
            {
                return Ok(categories);
            }
            else
            {
                return NotFound("No categories found");
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CategoryCreateDto categoryCreateDto)
        {
            var isAdded = await categoryRepository.AddCategory(categoryCreateDto);

            if (isAdded)
            {
                return StatusCode(StatusCodes.Status201Created);
            }
            else
            {
                return BadRequest("Category could not be added");
            }

        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await categoryRepository.DeleteCategory(id);

            if (isDeleted)
            {
                return Ok("Category deleted successfully");
            }
            else
            {
                return BadRequest("Category could not be deleted");
            }
        }
    }
}
