using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Services
{
    public interface ICuotaService
    {
        Task<List<Credito>> PorEstadosConContraparte(string nEstadoCuota, string nEstado);
    }
}
