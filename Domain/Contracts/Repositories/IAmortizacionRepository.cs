using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Repositories
{
    public interface IAmortizacionRepository : IRepository<Amortizacion>
    {
        Task<int> Cerrar(int AmortizacionID);
    }
}
