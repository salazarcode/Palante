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
    public class AmortizacionService : IAmortizacionService
    {
        private readonly IAmortizacionRepository _AmortizacionRepo;
        public AmortizacionService(IAmortizacionRepository AmortizacionRepo)
        {
            _AmortizacionRepo = AmortizacionRepo;
        }

        public async Task<List<Amortizacion>> All()
        {
            try
            {
                var res = await _AmortizacionRepo.All(new Paginacion() { });
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<int> Delete(int AmortizacionID)
        {
            try
            {
                var res = await _AmortizacionRepo.Delete(new Amortizacion() { AmortizacionID = AmortizacionID });
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Amortizacion> Find(int AmortizacionID)
        {
            try
            {
                var res = await _AmortizacionRepo.Find(new Amortizacion() { AmortizacionID = AmortizacionID });
                return res.First();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<int> Save(Amortizacion input)
        {
            try
            {
                var res = await _AmortizacionRepo.Save(input);
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
