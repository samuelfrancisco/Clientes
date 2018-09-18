using Clientes.Application.Clientes.Services.Interfaces;
using Clientes.Application.Clientes.ViewModels;
using Clientes.Domain.Clientes.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clientes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IServicoDePesquisaDeClientes _servicoDePesquisaDeClientes;
        private readonly IServicoDeCadastroDeCliente _servicoDeCadastroDeCliente;

        public ClientesController(IServicoDePesquisaDeClientes servicoDePesquisaDeClientes, IServicoDeCadastroDeCliente servicoDeCadastroDeCliente)
        {
            _servicoDePesquisaDeClientes = servicoDePesquisaDeClientes;
            _servicoDeCadastroDeCliente = servicoDeCadastroDeCliente;
        }

        [Route("")]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ClienteDTO>))]
        public async Task<IActionResult> GetAsync([FromQuery] FiltrosDaPesquisaDeClientesDTO filtros)
        {
            var clientes = await _servicoDePesquisaDeClientes.PesquisarAsync(filtros).ConfigureAwait(false);

            return Ok(clientes);
        }

        [Route("{id}")]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ClienteDTO))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var cliente = await _servicoDePesquisaDeClientes.ObterPeloIdAsync(id).ConfigureAwait(false);

            if (cliente == null)
                return NotFound();

            return Ok(cliente);
        }

        [Route("")]
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ClienteDTO))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> PostAsync([FromBody] CadastroDeClienteViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _servicoDeCadastroDeCliente.RegistrarNovoClienteAsync(viewModel).ConfigureAwait(false);

            if (result.IsFailure)
                return BadRequest(result);

            var cliente = result.Value;

            return CreatedAtAction("GetByIdAsync", new { id = cliente.Id }, cliente);
        }

        [Route("{id}")]
        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] CadastroDeClienteViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _servicoDeCadastroDeCliente.AlterarDadosDoCliente(id, viewModel).ConfigureAwait(false);

            if (result == null)
                return NotFound();

            if (result.IsFailure)
                return BadRequest(result);

            return NoContent();
        }

        [Route("{id}")]
        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _servicoDeCadastroDeCliente.RemoverRegistroDoCliente(id).ConfigureAwait(false);

            if (result == null)
                return NotFound();

            return NoContent();
        }
    }
}