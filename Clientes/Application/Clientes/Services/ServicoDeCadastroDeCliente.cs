using Clientes.Application.Clientes.Services.Interfaces;
using Clientes.Application.Clientes.ViewModels;
using Clientes.Domain.Clientes;
using Clientes.Domain.Clientes.DTOs;
using Clientes.Domain.Clientes.Repositories.Interfaces;
using Clientes.Domain.Core;
using Clientes.Infra.Data.Interfaces;
using System;
using System.Threading.Tasks;

namespace Clientes.Application.Clientes.Services
{
    public class ServicoDeCadastroDeCliente : IServicoDeCadastroDeCliente
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClienteRepository _clienteRepository;

        public ServicoDeCadastroDeCliente(IUnitOfWork unitOfWork, IClienteRepository clienteRepository)
        {
            _unitOfWork = unitOfWork;
            _clienteRepository = clienteRepository;
        }
        
        public async Task<Result<ClienteDTO>> RegistrarNovoClienteAsync(CadastroDeClienteViewModel viewModel)
        {
            var result = Cliente.RegistrarNovoCliente(viewModel.Nome, viewModel.DataDeNascimento, viewModel.Salario);

            if (result.IsFailure)
                return result.ToResult<ClienteDTO>();

            var cliente = result.Value;

            await _clienteRepository.AdicionarAsync(cliente).ConfigureAwait(false);

            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);

            return result.ToResult(new ClienteDTO
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                DataDeNascimento = cliente.DataDeNascimento,
                Salario = cliente.Salario
            });
        }

        public async Task<Result<ClienteDTO>> AlterarDadosDoCliente(Guid idDoCliente, CadastroDeClienteViewModel viewModel)
        {
            var cliente = await _clienteRepository.ObterPeloIdAsync(idDoCliente).ConfigureAwait(false);

            if (cliente == null)
                return null;

            var result = cliente.AlterarDados(viewModel.Nome, viewModel.DataDeNascimento, viewModel.Salario);

            if (result.IsFailure)
                return result.ToResult<ClienteDTO>();

            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);

            return result.ToResult(new ClienteDTO
            {
                Id = cliente.Id,
                Nome = cliente.Nome,
                DataDeNascimento = cliente.DataDeNascimento,
                Salario = cliente.Salario
            });
        }

        public async Task<Result> RemoverRegistroDoCliente(Guid idDoCliente)
        {
            var cliente = await _clienteRepository.ObterPeloIdAsync(idDoCliente).ConfigureAwait(false);

            if (cliente == null)
                return null;

            _clienteRepository.Remover(cliente);

            await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);

            return new Result();
        }
    }
}
