using System;
using System.Threading.Tasks;

namespace Clientes.Infra.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {        
        Task SaveChangesAsync();
    }
}
