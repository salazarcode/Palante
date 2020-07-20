using Domain.Contracts;
using Domain.Contracts.Repositories;
using Domain.Contracts.Services;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace Domain.Services
{
    public class CuotaService : ICuotaService
    {
        private readonly ICuotaRepository _cuotaRepo;
        public CuotaService(ICuotaRepository cuotaRepo)
        {
            _cuotaRepo = cuotaRepo;
        }

        public async Task<List<Cuota>> GetCuotas(DateTime pagosDesde, DateTime pagosHasta, string nEstadoCuota)
        {
            try
            {
                var res = await _cuotaRepo.GetCuotas(pagosDesde, pagosHasta, nEstadoCuota);
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<Cuota>> GetCuotasPorVencer(DateTime pagosDesde, DateTime pagosHasta, string codigoFondeador)
        {
            try
            {
                var res = await _cuotaRepo.GetCuotasPorVencer(pagosDesde, pagosHasta, codigoFondeador);
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
