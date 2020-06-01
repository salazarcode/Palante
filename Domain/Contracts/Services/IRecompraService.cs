using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Services
{
    public interface IRecompraService
    {
        Task<int> Save(Recompra cartera);
        Task<IEnumerable<Recompra>> All();
        Task<Recompra> Find(int RecompraID);
        Task<int> Add(int RecompraID, int nCodCred);
        Task<int> Remove(int RecompraID, int nCodCred);
        Task<int> Cerrar(int RecompraID, DateTime FechaCierre);
        Task<int> Delete(int RecompraID);
    }
}
