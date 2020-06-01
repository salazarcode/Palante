using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Services
{
    public interface IPagoService
    {
        Task<int> Save(Pago pago);
        Task<IEnumerable<Pago>> All();
        Task<int> Add(int PagoID, int nCodCred, int nNroCuota);
        Task<int> Remove(int PagoID, int nCodCred, int nNroCuota);
        Task<Pago> Find(int PagoID);
        Task<int> Cerrar(int PagoID, DateTime FechaCierre);
        Task<int> Delete(int PagoID);
    }
}
