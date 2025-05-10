using API.Data.Models;
using API.Data.Models.DTOs.Product;
using API.Repository;
using API.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public ProductsController(IProductRepository productRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.productRepository = productRepository;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] ProductPostDto productPostDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (productPostDto == null)
            {
                return BadRequest(ModelState);
            }

            var product = mapper.Map<Product>(productPostDto);

            //Subida de Archivo
            if (productPostDto.Image != null)
            {
                string nombreArchivo = System.Guid.NewGuid().ToString() + Path.GetExtension(productPostDto.Image.FileName);
                string rutaArchivo = @"wwwroot\ImagenesProductos\" + nombreArchivo;

                var ubicacionDirectorio = Path.Combine(Directory.GetCurrentDirectory(), rutaArchivo);

                using (var fileStream = new FileStream(ubicacionDirectorio, FileMode.Create))
                {
                    await productPostDto.Image.CopyToAsync(fileStream);
                }

                var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                product.ImageUrl = baseUrl + "/ImagenesProductos/" + nombreArchivo;
            }
            else
            {
                product.ImageUrl = "https://placehold.co/600x400";
            }

            var isAdded = await productRepository.AddProduct(product);

            if (isAdded)
            {
                return StatusCode(StatusCodes.Status201Created);
            }
            else
            {
                return BadRequest("Product could not be added");
            }
        }

        [HttpGet("Filter")]
        public async Task<IActionResult> Get(string? productType=null, int? categoryId = null)
        {
            var products = await productRepository.GetProducts(productType, categoryId);

            if (products.Any())
            {
                return Ok(products);
            }
            else
            {
                return NotFound("No products found");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await productRepository.DeleteProduct(id);
            if (isDeleted)
            {
                return Ok("Product deleted successfully");
            }
            else
            {
                return BadRequest("Product could not be deleted");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{productId}", Name = "UpdateProduct")]
        public async Task<IActionResult> Put(int productId, [FromForm] ProductUpdateDto productUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (productUpdateDto == null || productId != productUpdateDto.Id)
            {
                return BadRequest(ModelState);
            }

            var productExistente = productRepository.GetProduct(productId);

            if (productExistente == null)
            {
                return NotFound($"No se encontra el producto con ID: {productId}");
            }

            var product = mapper.Map<Product>(productUpdateDto);

            if (product.Image != null)
            {
                string nombreArchivo = System.Guid.NewGuid().ToString() + Path.GetExtension(productUpdateDto.Image.FileName);
                string rutaArchivo = @"wwwroot\ImagenesProductos\" + nombreArchivo;

                var ubicacionDirectorio = Path.Combine(Directory.GetCurrentDirectory(), rutaArchivo);

                using (var fileStream = new FileStream(ubicacionDirectorio, FileMode.Create))
                {
                    await productUpdateDto.Image.CopyToAsync(fileStream);
                }

                var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.Value}{HttpContext.Request.PathBase.Value}";
                product.ImageUrl = baseUrl + "/ImagenesProductos/" + nombreArchivo;
            }

            else

            {
                product.ImageUrl = "https://placehold.co/600x400";
            }

            var isUpdated = await productRepository.UpdateProduct(product);
            if (isUpdated)
            {
                return Ok("Product updated successfully");
            }
            else
            {
                return BadRequest("Product could not be updated");
            }
        }

        [HttpGet("GetProductByname")]
        public async Task<IActionResult> GetProductName(string nombre)
        {
            try
            {
                var resultado = await productRepository.GetProductByName(nombre);

                if (resultado.Any())
                {
                    var productGetDtos = mapper.Map<List<ProductGetDto>>(resultado);
                    return Ok(productGetDtos);
                }

                return NotFound($"No se encontraron resultados para la búsqueda: {nombre}");

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error recuperando datos");
            }
        }
    }
}
