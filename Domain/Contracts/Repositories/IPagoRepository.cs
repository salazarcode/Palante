using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Repositories
{
    public interface IPagoRepository : IRepository<Pago>
    {
        Task<int> Cerrar(int PagoID, DateTime FechaCierre);
        Task<int> Add(int PagoID, int nCodCred, int nNroCuota);
        Task<int> Remove(int PagoID, int nCodCred, int nNroCuota);
    }
}
