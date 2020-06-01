using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GestionCartera.API.ValueObjects;
using Transversal.Util;

namespace GestionCartera.API.Controllers
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
                    Creditos = new List<Credito>(),
                    ProductoID = carteraVO.ProductoID
                };

                if (carteraVO.Creditos != "" && carteraVO.Creditos != null)
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
        public async Task<int> Remove([FromForm] int CarteraID, [FromForm] int ProductoID, [FromForm]  int CreditoID)
        {
            try
            {
                var res = await _CarteraService.Remove(CarteraID, ProductoID, CreditoID);
                return res;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        /// <summary>
        /// Cerrar Cartera
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Cerrar")]
        public async Task<int> Cerrar([FromForm] int CarteraID, [FromForm] int ProductoID, [FromForm] DateTime FechaCierre)
        {
            try
            {
                var res = await _CarteraService.Cerrar(CarteraID, ProductoID, FechaCierre);
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
        public async Task<int> Add(int CarteraID, int ProductoID, int CreditoID)
        {
            try
            {
                var res = await _CarteraService.Add(CarteraID, ProductoID, CreditoID);
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
        public async Task<IEnumerable<Cartera>> All(int ProductoID, int EsRepro)
        {
            try
            {
                var res = await _CarteraService.All(ProductoID, EsRepro);
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
        public async Task<Cartera> Find(int CarteraID, int ProductoID)
        {
            try
            {
                var res = await _CarteraService.Find(CarteraID, ProductoID);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        /// Eliminar cartera
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Eliminar")]
        public async Task<int> Eliminar(int CarteraID, int ProductoID)
        {
            try
            {
                var res = await _CarteraService.Delete(CarteraID, ProductoID);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}