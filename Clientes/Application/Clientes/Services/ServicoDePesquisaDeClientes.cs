using Clientes.Application.Clientes.Services.Interfaces;
using Clientes.Domain.Clientes.DTOs;
using Clientes.Domain.Clientes.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clientes.Application.Clientes.Services
{
    public class ServicoDePesquisaDeClientes : IServicoDePesquisaDeClientes
    {
        private readonly IClienteRepository _clienteRepository;

        public ServicoDePesquisaDeClientes(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }        

        public Task<IEnumerable<ClienteDTO>> PesquisarAsync(FiltrosDaPesquisaDeClientesDTO filtros)
        {
            return _clienteRepository.PesquisarAsync(filtros);
        }

        public async Task<ClienteDTO> ObterPeloIdAsync(Guid id)
        {
            var cliente = await _clienteRepository.ObterPeloIdAsync(id).ConfigureAwait(false);

            if (cliente == null)
                return null;

            return new ClienteDTO
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                DataDeNascimento = cliente.DataDeNascimento,
                Salario = cliente.Salario
            };
        }
    }
}
