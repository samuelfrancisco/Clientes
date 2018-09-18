using Clientes.Domain.Clientes.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clientes.Domain.Clientes.Repositories.Interfaces
{
    public interface IClienteRepository
    {
        Task<IEnumerable<ClienteDTO>> PesquisarAsync(FiltrosDaPesquisaDeClientesDTO filtros);
        Task<Cliente> ObterPeloIdAsync(Guid id);
        Task AdicionarAsync(Cliente cliente);
        void Remover(Cliente cliente);
    }
}
