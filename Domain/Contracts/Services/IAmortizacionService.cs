using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Services
{
    public interface IAmortizacionService
    {
        Task<List<Amortizacion>> All();
        Task<int> Save(Amortizacion Amortizacion);
        Task<int> Delete(int AmortizacionID);
        Task<Amortizacion> Find(int AmortizacionID);
    }
}
