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
    public class ReprogramacionService : IReprogramacionService
    {
        private readonly IReprogramacionRepository _ReprogramacionRepo;
        public ReprogramacionService(IReprogramacionRepository ReprogramacionRepo)
        {
            _ReprogramacionRepo = ReprogramacionRepo;
        }

        public async Task<List<Reprogramacion>> All()
        {
            try
            {
                var res = await _ReprogramacionRepo.All(new Paginacion() { });
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<int> Delete(int ReprogramacionID)
        {
            try
            {
                var res = await _ReprogramacionRepo.Delete(new Reprogramacion() { ReprogramacionID = ReprogramacionID});
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Reprogramacion> Find(int ReprogramacionID)
        {
            try
            {
                var res = await _ReprogramacionRepo.Find(new Reprogramacion() { ReprogramacionID = ReprogramacionID });
                return res.First();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<int> Save(Reprogramacion input)
        {
            try
            {
                var res = await _ReprogramacionRepo.Save(input);
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
