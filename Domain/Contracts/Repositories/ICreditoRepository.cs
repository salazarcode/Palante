using Domain.Entities;
using Domain.ValueObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Contracts.Repositories
{
    public interface ICreditoRepository : IRepository<Credito>
    {
        Task<List<Credito>> Cumplimiento(int FondeadorID, string creditos);
        Task<List<Credito>> Search(CreditoSearch search);
        Task<List<CreditoVO>> PorEstado(string EstadosConcatenadosComa);
    }
}
