using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Repositories
{
    public interface IPagoRepository : IRepository<Pago>
    {
        Task<int> Crear(Pago pago);
        Task<int> Confirmar(int PagoID);
        Task<List<PagoDetalle>> FindDeuda(int nCodCred);
    }
}
