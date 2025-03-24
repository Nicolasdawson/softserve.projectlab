using API.Models.IntAdmin;
using API.Services.IntAdmin;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace API.Controllers.IntAdmin
{
    /// <summary>
    /// API Controller for managing Catalog operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogService _catalogService;

        /// <summary>
        /// Constructor with dependency injection for ICatalogService.
        /// </summary>
        public CatalogController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        /// <summary>
        /// Adds a new catalog.
        /// </summary>
        /// <param name="catalog">Catalog object to add</param>
        /// <returns>HTTP response with the created catalog or error message</returns>
        [HttpPost("add")]
        public async Task<IActionResult> AddCatalog([FromBody] Catalog catalog)
        {
            var result = await _catalogService.AddCatalogAsync(catalog);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Updates an existing catalog.
        /// </summary>
        /// <param name="catalog">Catalog object with updated data</param>
        /// <returns>HTTP response with the updated catalog or error message</returns>
        [HttpPut("update")]
        public async Task<IActionResult> UpdateCatalog([FromBody] Catalog catalog)
        {
            var result = await _catalogService.UpdateCatalogAsync(catalog);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        /// <summary>
        /// Retrieves a catalog by its unique ID.
        /// </summary>
        /// <param name="catalogId">Unique identifier of the catalog</param>
        /// <returns>HTTP response with the catalog or error message</returns>
        [HttpGet("{catalogId}")]
        public async Task<IActionResult> GetCatalogById(int catalogId)
        {
            var result = await _catalogService.GetCatalogByIdAsync(catalogId);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        /// <summary>
        /// Retrieves all catalogs.
        /// </summary>
        /// <returns>HTTP response with the list of catalogs or error message</returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetAllCatalogs()
        {
            var result = await _catalogService.GetAllCatalogsAsync();
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Removes a catalog by its unique ID.
        /// </summary>
        /// <param name="catalogId">Unique identifier of the catalog to remove</param>
        /// <returns>HTTP response indicating success or failure</returns>
        [HttpDelete("remove/{catalogId}")]
        public async Task<IActionResult> RemoveCatalog(int catalogId)
        {
            var result = await _catalogService.RemoveCatalogAsync(catalogId);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }
    }
}
