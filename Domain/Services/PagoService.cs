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
    public class PagoService : IPagoService
    {
        private readonly IPagoRepository _pagoRepo;
        public PagoService(IPagoRepository pagoRepo)
        {
            _pagoRepo = pagoRepo;
        }

        public async Task<List<Pago>> All(Pago pago)
        {
            try
            {
                var res = await _pagoRepo.All(new Paginacion() { });
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Pago> Find(Pago pago)
        {
            try
            {
                var res = await _pagoRepo.Find(pago);
                return res.First();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        async Task<int> IPagoService.Save(Pago pago)
        {
            try
            {
                var res = await _pagoRepo.Save(pago);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        async Task<int> IPagoService.Delete(Pago pago)
        {
            try
            {
                var res = await _pagoRepo.Delete(pago);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<PagoDetalle>> FindDeuda(int nCodCred)
        {
            try
            {
                var res = await _pagoRepo.FindDeuda(nCodCred);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> Confirmar(int PagoID)
        {
            try
            {
                var res = await _pagoRepo.Confirmar(PagoID);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
