using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Repositories
{
    public interface IRecompraRepository : IRepository<Recompra>
    {
        Task<int> Cerrar(int RecompraID, DateTime FechaCierre);
        Task<int> Add(int RecompraID, int nCodCred);
        Task<int> Remove(int RecompraID, int nCodCred);
    }
}
