using API.Models.Customers;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers.Customers
{
    [ApiController]
    [Route("api/linescredit")]
    public class LineOfCreditController : ControllerBase
    {
        private readonly ILineOfCreditService _lineOfCreditService;

        public LineOfCreditController(ILineOfCreditService lineOfCreditService)
        {
            _lineOfCreditService = lineOfCreditService;
        }

        /// <summary>
        /// Crea una nueva línea de crédito para un cliente.
        /// </summary>
        /// <param name="lineOfCredit">Datos de la línea de crédito</param>
        /// <param name="customerId">ID del cliente</param>
        /// <returns>La línea de crédito creada</returns>
        [HttpPost("{customerId}")]
        public async Task<IActionResult> CreateLineOfCredit([FromBody] LineOfCredit lineOfCredit, int customerId)
        {
            var result = await _lineOfCreditService.AddLineOfCreditAsync(lineOfCredit, customerId);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Obtiene una línea de crédito por su ID.
        /// </summary>
        /// <param name="id">ID de la línea de crédito</param>
        /// <returns>La línea de crédito encontrada</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLineOfCreditById(string id)
        {
            var result = await _lineOfCreditService.GetLineOfCreditByIdAsync(id);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        /// <summary>
        /// Obtiene la línea de crédito de un cliente por su ID.
        /// </summary>
        /// <param name="customerId">ID del cliente</param>
        /// <returns>La línea de crédito del cliente</returns>
        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetLineOfCreditByCustomerId(int customerId)
        {
            var result = await _lineOfCreditService.GetLineOfCreditByCustomerIdAsync(customerId);
            return result.IsSuccess ? Ok(result.Data) : NotFound(result.ErrorMessage);
        }

        /// <summary>
        /// Actualiza una línea de crédito.
        /// </summary>
        /// <param name="id">ID de la línea de crédito</param>
        /// <param name="lineOfCredit">Datos actualizados de la línea de crédito</param>
        /// <returns>La línea de crédito actualizada</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLineOfCredit(string id, [FromBody] LineOfCredit lineOfCredit)
        {
            if (id != lineOfCredit.Id)
            {
                return BadRequest("ID de la línea de crédito no coincide");
            }

            var result = await _lineOfCreditService.UpdateLineOfCreditAsync(lineOfCredit);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Elimina una línea de crédito.
        /// </summary>
        /// <param name="id">ID de la línea de crédito</param>
        /// <returns>Resultado de la operación</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLineOfCredit(string id)
        {
            var result = await _lineOfCreditService.DeleteLineOfCreditAsync(id);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Obtiene todas las transacciones de una línea de crédito.
        /// </summary>
        /// <param name="id">ID de la línea de crédito</param>
        /// <returns>Lista de transacciones</returns>
        [HttpGet("{id}/transactions")]
        public async Task<IActionResult> GetTransactions(string id)
        {
            var result = await _lineOfCreditService.GetTransactionsAsync(id);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }

        /// <summary>
        /// Agrega una transacción a una línea de crédito.
        /// </summary>
        /// <param name="id">ID de la línea de crédito</param>
        /// <param name="transaction">Datos de la transacción</param>
        /// <returns>La transacción creada</returns>
        [HttpPost("{id}/transactions")]
        public async Task<IActionResult> AddTransaction(string id, [FromBody] CreditTransaction transaction)
        {
            var result = await _lineOfCreditService.AddTransactionAsync(id, transaction);
            return result.IsSuccess ? Ok(result.Data) : BadRequest(result.ErrorMessage);
        }
    }
}