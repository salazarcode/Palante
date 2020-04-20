using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Contracts.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.API.ValueObjects;

namespace Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarterasController : ControllerBase
    {
        private readonly ICarteraService _CarteraService;

        public CarterasController(ICarteraService CarteraService)
        {
            _CarteraService = CarteraService;
        }

        /// <summary>
        /// Listar todos los Creditos
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Save")]
        public async Task<int> Save([FromForm] SaveCarteraVO carteraVO)
        {
            try
            {
                Cartera cartera = new Cartera
                {
                    CarteraID = carteraVO.CarteraID,
                    Fondeador = new Fondeador { 
                        FondeadorID = carteraVO.FondeadorID
                    },
                    Creditos = new List<Credito>()
                };

                if (carteraVO.Creditos != "")
                    carteraVO.Creditos.Split(",").ToList().ForEach(x => cartera.Creditos.Add(new Credito { 
                        nCodCred = Convert.ToInt32(x)
                    }));

                int NewCarteraID = await _CarteraService.Save(cartera);

                return NewCarteraID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Quitar Crédito de una cartera
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Remove")]
        public async Task<int> Remove(int CarteraID, int CreditoID)
        {
            try
            {
                var res = await _CarteraService.Add(CarteraID, CreditoID);
                return res;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Agregar Crédito
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Add")]
        public async Task<int> Add(int CarteraID, int CreditoID)
        {
            try
            {
                var res = await _CarteraService.Add(CarteraID, CreditoID);
                return res;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Listar todos las carteras
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("All")]
        public async Task<IEnumerable<Cartera>> All()
        {
            try
            {
                var res = await _CarteraService.All();
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        /// Detalle de Cartera
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Find")]
        public async Task<Cartera> Find(int CarteraID)
        {
            try
            {
                var res = await _CarteraService.Find(CarteraID);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}