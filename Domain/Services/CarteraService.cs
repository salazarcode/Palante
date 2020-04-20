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
    public class CarteraService : ICarteraService
    {
        private readonly ICarteraRepository _CarteraRepo;
        public CarteraService(ICarteraRepository CarteraRepo)
        {
            _CarteraRepo = CarteraRepo;
        }

        async Task<int> ICarteraService.Save(Cartera cartera)
        {
            try
            {
                var res = await _CarteraRepo.Save(cartera);
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        async Task<int> ICarteraService.Add(int CarteraID, int CreditoID)
        {
            try
            {
                var res = await _CarteraRepo.Add(CarteraID, CreditoID);
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        async Task<int> ICarteraService.Remove(int CarteraID, int CreditoID)
        {
            try
            {
                var res = await _CarteraRepo.Remove(CarteraID, CreditoID);
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<Cartera>> All()
        {
            try
            {
                var res = await _CarteraRepo.All();
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Cartera> Find(int CarteraID)
        {
            try
            {
                var res = await _CarteraRepo.Find(CarteraID);
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
