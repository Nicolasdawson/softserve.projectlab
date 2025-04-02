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
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public ClientController(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;  
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetClients()
        {
            var listClients = _clientRepository.GetClients();

            var listClientsDto = new List<ClientDtoOut>();

            foreach (var client in listClients)
            {
                listClientsDto.Add(_mapper.Map<ClientDtoOut>(client));
            }

            return Ok(listClientsDto);
        }

        [HttpGet("{clientId:int}", Name = "GetClient")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetClient(int clientId)
        {
            var client = _clientRepository.GetClient(clientId);

            if (client == null)
            {
                return NotFound();
            }

            var clientDtoOut = _mapper.Map<ClientDtoOut>(client);
            return Ok(clientDtoOut);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult CreateClient([FromBody] ClientDtoIn clientDtoIn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (clientDtoIn == null)
            {
                return BadRequest(ModelState);
            }

            if (_clientRepository.ClientExists(clientDtoIn.Email))
            {
                ModelState.AddModelError("email", "El email ya existe");
                return StatusCode(404, ModelState);
            }

            var client = _mapper.Map<Client>(clientDtoIn);

            if (!_clientRepository.CreateClient(client))
            {
                ModelState.AddModelError("", $"Algo salió mal guardando el registro {client.Email}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetClient", new { clientId = client.Id }, _mapper.Map<ClientDtoOut>(client));
        }
    }
}
