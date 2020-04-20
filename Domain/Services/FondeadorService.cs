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
    public class FondeadorService : IFondeadorService
    {
        private readonly IFondeadorRepository _FondeadorRepo;
        public FondeadorService(IFondeadorRepository FondeadorRepo)
        {
            _FondeadorRepo = FondeadorRepo;
        }

        async Task<List<Fondeador>> IFondeadorService.All()
        {
            try
            {
                var res = await _FondeadorRepo.All();
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        async Task<Fondeador> IFondeadorService.Find(int FondeadorID)
        {
            try
            {
                var res = await _FondeadorRepo.Find(FondeadorID);
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        async Task<int> IFondeadorService.Save(Fondeador fondeador)
        {
            try
            {
                var res = await _FondeadorRepo.Save(fondeador);
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        async Task<int> IFondeadorService.Delete(int FondeadorID)
        {
            try
            {
                var res = await _FondeadorRepo.Delete(FondeadorID);
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
