using API.Models.Customers;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers.Customers
{
    [ApiController]
    [Route("api/packages")]
    public class PackageController : ControllerBase
    {
        private readonly IPackageService _packageService;

        public PackageController(IPackageService packageService)
        {
            _packageService = packageService;
        }

        /// <summary>
        /// Crea un nuevo paquete para un cliente.
        /// </summary>
        /// <param name="package">Datos del paquete</param>
        /// <param name="customerId">ID del cliente</param>
        /// <returns>El paquete creado</returns>
        [HttpPost("customer/{customerId}")]
        public async Task<IActionResult> CreatePackage([FromBody] Package package, int customerId)
        {
            var result = await _packageService.CreatePackageAsync(package, customerId);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Obtiene un paquete por su ID.
        /// </summary>
        /// <param name="id">ID del paquete</param>
        /// <returns>El paquete encontrado</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPackageById(string id)
        {
            var result = await _packageService.GetPackageByIdAsync(id);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        /// <summary>
        /// Obtiene todos los paquetes de un cliente por su ID.
        /// </summary>
        /// <param name="customerId">ID del cliente</param>
        /// <returns>Lista de paquetes del cliente</returns>
        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetPackagesByCustomerId(int customerId)
        {
            var result = await _packageService.GetPackagesByCustomerIdAsync(customerId);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        /// <summary>
        /// Actualiza un paquete.
        /// </summary>
        /// <param name="id">ID del paquete</param>
        /// <param name="package">Datos actualizados del paquete</param>
        /// <returns>El paquete actualizado</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePackage(string id, [FromBody] Package package)
        {
            if (id != package.Id)
            {
                return BadRequest("ID del paquete no coincide");
            }

            var result = await _packageService.UpdatePackageAsync(package);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Elimina un paquete.
        /// </summary>
        /// <param name="id">ID del paquete</param>
        /// <returns>Resultado de la operación</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePackage(string id)
        {
            var result = await _packageService.DeletePackageAsync(id);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Añade un item al paquete.
        /// </summary>
        /// <param name="id">ID del paquete</param>
        /// <param name="itemSku">SKU del item</param>
        /// <param name="quantity">Cantidad a añadir</param>
        /// <returns>El paquete actualizado</returns>
        [HttpPost("{id}/items/{itemSku}")]
        public async Task<IActionResult> AddItemToPackage(string id, int itemSku, [FromQuery] int quantity = 1)
        {
            var result = await _packageService.AddItemToPackageAsync(id, itemSku, quantity);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Elimina un item del paquete.
        /// </summary>
        /// <param name="id">ID del paquete</param>
        /// <param name="itemSku">SKU del item</param>
        /// <returns>El paquete actualizado</returns>
        [HttpDelete("{id}/items/{itemSku}")]
        public async Task<IActionResult> RemoveItemFromPackage(string id, int itemSku)
        {
            var result = await _packageService.RemoveItemFromPackageAsync(id, itemSku);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Añade una nota al paquete.
        /// </summary>
        /// <param name="id">ID del paquete</param>
        /// <param name="note">Datos de la nota</param>
        /// <returns>El paquete actualizado</returns>
        [HttpPost("{id}/notes")]
        public async Task<IActionResult> AddNoteToPackage(string id, [FromBody] PackageNote note)
        {
            var result = await _packageService.AddNoteToPackageAsync(id, note);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Actualiza el estado del paquete.
        /// </summary>
        /// <param name="id">ID del paquete</param>
        /// <param name="status">Nuevo estado</param>
        /// <param name="updatedBy">Usuario que actualiza</param>
        /// <param name="notes">Notas para la actualización</param>
        /// <returns>El paquete actualizado</returns>
        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdatePackageStatus(string id, [FromQuery] string status, [FromQuery] string updatedBy, [FromQuery] string notes = "")
        {
            var result = await _packageService.UpdatePackageStatusAsync(id, status, updatedBy, notes);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Calcula el precio total del paquete.
        /// </summary>
        /// <param name="id">ID del paquete</param>
        /// <returns>El precio total</returns>
        [HttpGet("{id}/totalPrice")]
        public async Task<IActionResult> CalculateTotalPrice(string id)
        {
            var result = await _packageService.CalculateTotalPriceAsync(id);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Calcula el precio con descuento del paquete.
        /// </summary>
        /// <param name="id">ID del paquete</param>
        /// <returns>El precio con descuento</returns>
        [HttpGet("{id}/discountedPrice")]
        public async Task<IActionResult> CalculateDiscountedPrice(string id)
        {
            var result = await _packageService.CalculateDiscountedPriceAsync(id);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Calcula el valor total del contrato.
        /// </summary>
        /// <param name="id">ID del paquete</param>
        /// <returns>El valor total del contrato</returns>
        [HttpGet("{id}/contractValue")]
        public async Task<IActionResult> CalculateTotalContractValue(string id)
        {
            var result = await _packageService.CalculateTotalContractValueAsync(id);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Verifica si el contrato está activo.
        /// </summary>
        /// <param name="id">ID del paquete</param>
        /// <returns>Estado de actividad del contrato</returns>
        [HttpGet("{id}/isContractActive")]
        public async Task<IActionResult> IsContractActive(string id)
        {
            var result = await _packageService.IsContractActiveAsync(id);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Obtiene el tiempo restante del contrato.
        /// </summary>
        /// <param name="id">ID del paquete</param>
        /// <returns>Tiempo restante del contrato</returns>
        [HttpGet("{id}/remainingContractTime")]
        public async Task<IActionResult> GetRemainingContractTime(string id)
        {
            var result = await _packageService.GetRemainingContractTimeAsync(id);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }
    }
}