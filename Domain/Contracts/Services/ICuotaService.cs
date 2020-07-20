using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Services
{
    public interface ICuotaService
    {
        Task<List<Cuota>> GetCuotas(DateTime pagosDesde, DateTime pagosHasta, string nEstadoCuota);
    }
}
