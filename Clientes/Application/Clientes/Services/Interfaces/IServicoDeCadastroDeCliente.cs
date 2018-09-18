using Clientes.Application.Clientes.ViewModels;
using Clientes.Domain.Clientes.DTOs;
using Clientes.Domain.Core;
using System;
using System.Threading.Tasks;

namespace Clientes.Application.Clientes.Services.Interfaces
{
    public interface IServicoDeCadastroDeCliente
    {
        Task<Result<ClienteDTO>> RegistrarNovoClienteAsync(CadastroDeClienteViewModel viewModel);
        Task<Result<ClienteDTO>> AlterarDadosDoCliente(Guid idDoCliente, CadastroDeClienteViewModel viewModel);
        Task<Result> RemoverRegistroDoCliente(Guid idDoCliente);
    }
}
