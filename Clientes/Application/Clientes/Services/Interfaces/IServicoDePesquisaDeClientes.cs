using Clientes.Domain.Clientes.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clientes.Application.Clientes.Services.Interfaces
{
    public interface IServicoDePesquisaDeClientes
    {
        Task<IEnumerable<ClienteDTO>> PesquisarAsync(FiltrosDaPesquisaDeClientesDTO filtros);
        Task<ClienteDTO> ObterPeloIdAsync(Guid id);
    }
}
