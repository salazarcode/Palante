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

        public async Task<int> Add(int PagoID, int a, int b)
        {
            try
            {
                var res = await _pagoRepo.Add(PagoID, a, b);
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Task<int> Delete(int PagoID)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Remove(int PagoID, int a, int b)
        {
            try
            {
                var res = await _pagoRepo.Remove(PagoID, a, b);
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        async Task<IEnumerable<Pago>> IPagoService.All()
        {
            try
            {
                var res = await _pagoRepo.All();
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        async Task<int> IPagoService.Cerrar(int PagoID, DateTime FechaCierre)
        {
            try
            {
                var res = await _pagoRepo.Cerrar(PagoID, FechaCierre);
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        async Task<Pago> IPagoService.Find(int PagoID)
        {
            try
            {
                var res = await _pagoRepo.Find(new Pago() { PagoID = PagoID});
                return res.First();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        async Task<int> IPagoService.Save(Pago pago)
        {
            try
            {
                var res = await _pagoRepo.Save(pago);
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
