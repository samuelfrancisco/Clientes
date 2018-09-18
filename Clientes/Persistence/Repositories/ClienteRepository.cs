using Clientes.Domain.Clientes;
using Clientes.Domain.Clientes.DTOs;
using Clientes.Domain.Clientes.Repositories.Interfaces;
using Clientes.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clientes.Persistence.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ClientsContext _clientsContext;

        public ClienteRepository(ClientsContext clientsContext)
        {
            _clientsContext = clientsContext;
        }

        public async Task<IEnumerable<ClienteDTO>> PesquisarAsync(FiltrosDaPesquisaDeClientesDTO filtros)
        {
            var query = _clientsContext.Clientes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtros.Nome))
                query = query.Where(x => x.Nome.ToUpper().Contains(filtros.Nome.ToUpper()));

            if (filtros.DataDeNascimentoDe.HasValue)
                query = query.Where(x => x.DataDeNascimento >= filtros.DataDeNascimentoDe);

            if (filtros.DataDeNascimentoAte.HasValue)
                query = query.Where(x => x.DataDeNascimento <= filtros.DataDeNascimentoAte);

            if (filtros.SalarioDe.HasValue)
                query = query.Where(x => x.Salario >= filtros.SalarioDe);

            if (filtros.SalarioAte.HasValue)
                query = query.Where(x => x.Salario <= filtros.SalarioAte);

            return await query.Select(x => new ClienteDTO
            {
                Id = x.Id,
                Nome = x.Nome,
                DataDeNascimento = x.DataDeNascimento,
                Salario = x.Salario
            }).ToListAsync()
            .ConfigureAwait(false);
        }

        public async Task<Cliente> ObterPeloIdAsync(Guid id)
        {
            return await _clientsContext.Clientes.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
        }

        public async Task AdicionarAsync(Cliente cliente)
        {
            await _clientsContext.Clientes.AddAsync(cliente).ConfigureAwait(false);
        }

        public void Remover(Cliente cliente)
        {
            _clientsContext.Clientes.Remove(cliente);
        }
    }
}
