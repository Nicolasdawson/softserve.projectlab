using API.Data.Models.DTOs;
using API.Data.Models;
using API.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetProducts()
        {
            var listProducts = _productRepository.GetProducts();

            var listProductsDto = new List<ProductDtoOut>();

            foreach (var product in listProducts)
            {
                listProductsDto.Add(_mapper.Map<ProductDtoOut>(product));
            }

            return Ok(listProductsDto);
        }

        [HttpGet("{productId:int}", Name = "GetProduct")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetProduct(int productId)
        {
            var product = _productRepository.GetProduct(productId);

            if (product == null)
            {
                return NotFound();
            }

            var productDtoOut = _mapper.Map<ProductDtoOut>(product);
            return Ok(productDtoOut);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult CreateProduct([FromBody] ProductDtoIn productDtoIn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (productDtoIn == null)
            {
                return BadRequest(ModelState);
            }

            if (_productRepository.ProductExists(productDtoIn.name))
            {
                ModelState.AddModelError("name", "El producto ya existe");
                return StatusCode(404, ModelState);
            }

            var product = _mapper.Map<Product>(productDtoIn);

            if (!_productRepository.CreateProduct(product))
            {
                ModelState.AddModelError("", $"Algo salió mal guardando el registro {product.Name}");
                return StatusCode(500, ModelState);
            }

            // Cargar la relación manualmente después de guardar
            product = _productRepository.GetProduct(product.Id);

            return CreatedAtRoute("GetProduct", new { productId = product.Id }, _mapper.Map<ProductDtoOut>(product));
        }
    }
}
