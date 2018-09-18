using Clientes.Infra.Data.Interfaces;
using Clientes.Persistence.Contexts;
using System.Threading.Tasks;

namespace Clientes.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ClientsContext _clientsContext;

        public UnitOfWork(ClientsContext clientsContext)
        {
            _clientsContext = clientsContext;
        }

        public void Dispose()
        {
            _clientsContext.Dispose();
        }

        public async Task SaveChangesAsync()
        {
            await _clientsContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
