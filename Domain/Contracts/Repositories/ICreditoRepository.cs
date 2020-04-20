using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Contracts.Repositories
{
    public interface ICreditoRepository : IRepository<Credito>
    {
        Task<List<Credito>> ByClienteID(int ClienteID);
        Task<List<Credito>> Cumplimiento(int FondeadorID);
    }
}
