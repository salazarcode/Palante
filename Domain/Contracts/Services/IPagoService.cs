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
        Task<Pago> Find(Pago pago);
        Task<int> Delete(Pago pago);
        Task<List<Pago>> All(Pago pago);
        Task<List<PagoDetalle>> FindDeuda(int nCodCred);

    }
}
