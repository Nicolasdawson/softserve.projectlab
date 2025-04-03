using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Data.Models;
using System.Collections.Generic;
using API.Repository.IRepository;
using API.Data.Models.DTOs;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IMapper _mapper;

        public ProductCategoryController(IProductCategoryRepository productCategoryRepository, IMapper mapper)
        {
            _productCategoryRepository = productCategoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetCategories()
        {
            var listCategories = _productCategoryRepository.GetCategories();

            var listCategoriesDto = new List<ProductCategoryDtoOut>();

            foreach (var category in listCategories)
            {
                listCategoriesDto.Add(_mapper.Map<ProductCategoryDtoOut>(category));
            }

            return Ok(listCategoriesDto);
        }


        [HttpGet("{categoryId:int}", Name = "GetCategory")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCategory(int categoryId)
        {
            var category = _productCategoryRepository.GetCategory(categoryId);

            if (category == null)
            {
                return NotFound();
            }

            var categoryDtoOut = _mapper.Map<ProductCategoryDtoOut>(category);
            return Ok(categoryDtoOut);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult CreateCategory([FromBody] ProductCategoryDtoIn productCategoryDtoIn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (productCategoryDtoIn == null)
            {
                return BadRequest(ModelState);
            }

            if (_productCategoryRepository.CategoryExists(productCategoryDtoIn.category))
            {
                ModelState.AddModelError("name", "La categoría ya existe");
                return StatusCode(404, ModelState);
            }

            var category = _mapper.Map<ProductCategory>(productCategoryDtoIn);

            if (!_productCategoryRepository.CreateCategory(category))
            {
                ModelState.AddModelError("", $"Algo salió mal guardando el registro {category.Category}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetCategory", new { categoryId = category.Id }, _mapper.Map<ProductCategoryDtoOut>(category));
        }

    }
}
