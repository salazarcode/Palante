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

        async Task<IEnumerable<Cuota>> ICuotaService.All()
        {
            try
            {
                var res = await _cuotaRepo.All();
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
