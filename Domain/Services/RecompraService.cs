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
    public class RecompraService : IRecompraService
    {
        private readonly IRecompraRepository _recompraRepo;
        public RecompraService(IRecompraRepository RecompraRepo)
        {
            _recompraRepo = RecompraRepo;
        }

        async Task<int> IRecompraService.Save(Recompra Recompra)
        {
            try
            {
                var res = await _recompraRepo.Save(Recompra);
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        async Task<int> IRecompraService.Add(int RecompraID, int nCodCred)
        {
            try
            {
                var res = await _recompraRepo.Add(RecompraID, nCodCred);
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        async Task<int> IRecompraService.Remove(int RecompraID, int nCodCred)
        {
            try
            {
                var res = await _recompraRepo.Remove(RecompraID, nCodCred);
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        async Task<IEnumerable<Recompra>> IRecompraService.All()
        {
            try
            {
                var res = await _recompraRepo.All();
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        async Task<Recompra> IRecompraService.Find(int RecompraID)
        {
            try
            {
                var res = await _recompraRepo.Find(new Recompra() { RecompraID = RecompraID });
                return res.First();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        async Task<int> IRecompraService.Cerrar(int RecompraID, DateTime FechaCierre)
        {
            try
            {
                var res = await _recompraRepo.Cerrar(RecompraID, FechaCierre);
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        async Task<int> IRecompraService.Delete(int RecompraID)
        {
            try
            {
                var res = await _recompraRepo.Delete(new Recompra() { RecompraID = RecompraID });
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
