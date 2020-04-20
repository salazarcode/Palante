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
    public class CreditoService : ICreditoService
    {
        private readonly ICreditoRepository _CreditoRepo;
        public CreditoService(ICreditoRepository CreditoRepo)
        {
            _CreditoRepo = CreditoRepo;
        }

        async Task<List<Credito>> ICreditoService.All()
        {
            try
            {

            var res = await _CreditoRepo.All();
            return res;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        async Task<List<Credito>> ICreditoService.ByClienteID(int ID)
        {
            var res = await _CreditoRepo.All();
            var Creditos = res.FindAll(x => x.nCodCred == ID);
            return Creditos;
        }

        async Task<List<Credito>> ICreditoService.Cumplimiento(int FondeadorID)
        {
            var res = await _CreditoRepo.Cumplimiento(FondeadorID);
            return res;
        }
    }
}
