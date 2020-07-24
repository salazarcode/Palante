using Domain.Contracts;
using Domain.Contracts.Repositories;
using Domain.Contracts.Services;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using Domain.ValueObjects;

namespace Domain.Services
{
    public class CreditoService : ICreditoService
    {
        private readonly ICreditoRepository _CreditoRepo;
        public CreditoService(ICreditoRepository CreditoRepo)
        {
            _CreditoRepo = CreditoRepo;
        }

        async Task<List<Credito>> ICreditoService.All(Paginacion pag = null)
        {
            try
            {

                var res = await _CreditoRepo.All(pag);
                return res;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        async Task<List<Credito>> ICreditoService.Cumplimiento(int FondeadorID, string creditos)
        {
            var res = await _CreditoRepo.Cumplimiento(FondeadorID, creditos);
            return res;
        }
        public async Task<List<Credito>> Search(CreditoSearch search)
        {
            try
            {
                var res = await _CreditoRepo.Search(search);
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<CreditoVO>> PorEstado(string EstadosConcatenadosComa)
        {
            try
            {
                var res = await _CreditoRepo.PorEstado(EstadosConcatenadosComa);
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<Credito>> PendientesPorAprobacion(int FondeadorID, DateTime desde, DateTime hasta)
        {
            try
            {
                var res = await _CreditoRepo.PendientesPorAprobacion(FondeadorID, desde, hasta);
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<Credito>> DisponiblesPorFondeador(int FondeadorID, DateTime desde, DateTime hasta)
        {
            try
            {
                var res = await _CreditoRepo.DisponiblesPorFondeador(FondeadorID, desde, hasta);
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<Credito>> CreditosFondeador(DateTime desde, DateTime hasta)
        {
            try
            {
                var res = await _CreditoRepo.CreditosFondeador(desde, hasta);
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
