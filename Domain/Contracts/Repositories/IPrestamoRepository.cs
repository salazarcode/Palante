using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Contracts.Repositories
{
    public interface IPrestamoRepository : IRepository<Prestamo>
    {
        Task<List<Prestamo>> ByClienteID(int ClienteID);
    }
}
