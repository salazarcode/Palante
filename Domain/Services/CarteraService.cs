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


        async Task<int> ICarteraService.Add(int CarteraID, int ProductoID, int CreditoID)
        {
            try
            {
                var res = await _CarteraRepo.Add(CarteraID, ProductoID, CreditoID);
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        async Task<int> ICarteraService.Remove(int CarteraID, int ProductoID, int CreditoID)
        {
            try
            {
                var res = await _CarteraRepo.Remove(CarteraID, ProductoID, CreditoID);
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        async Task<IEnumerable<Cartera>> ICarteraService.All(int ProductoID, int EsRepro)
        {
            try
            {
                Paginacion pag = new Paginacion()
                {
                    producto = ProductoID
                };

                if (EsRepro != 0)
                    pag.repro = true;

                var res = await _CarteraRepo.All(pag);

                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        async Task<Cartera> ICarteraService.Find(int CarteraID, int ProductoID)
        {
            try
            {
                var res = await _CarteraRepo.Find(new Cartera() { 
                    CarteraID = CarteraID, 
                    ProductoID = ProductoID 
                });
                return res.First();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        async Task<int> ICarteraService.Cerrar(int CarteraID, int ProductoID, DateTime FechaCierre)
        {
            try
            {
                var res = await _CarteraRepo.Cerrar(CarteraID, ProductoID, FechaCierre);
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        async Task<int> ICarteraService.Delete(int CarteraID, int ProductoID)
        {
            try
            {
                var res = await _CarteraRepo.Delete(new Cartera()
                {
                    CarteraID = CarteraID,
                    ProductoID = ProductoID
                });
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
