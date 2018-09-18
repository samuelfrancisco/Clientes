using Clientes.Domain.Clientes;
using Microsoft.EntityFrameworkCore;

namespace Clientes.Persistence.Contexts
{
    public class ClientsContext : DbContext
    {
        public ClientsContext(DbContextOptions<ClientsContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
    }
}
