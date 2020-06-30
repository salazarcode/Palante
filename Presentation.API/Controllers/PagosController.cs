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
    [Route("[controller]")]
    [ApiController]
    public class PagosController : ControllerBase
    {
        private readonly IPagoService _PagoService;

        public PagosController(IPagoService PagoService)
        {
            _PagoService = PagoService;
        }

    

        /// <summary>
        /// Listar todos los Creditos
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Save")]
        public async Task<int> Save([FromForm] SavePagoVO pago)
        {
            try
            {
                List<string> lista = pago.Pagos.Split(";").ToList();
                List<PagoDetalle> detalles = lista.Select(x =>
                {
                    var res = x.Split(",");
                    PagoDetalle det = new PagoDetalle();
                    det.nCodCred = Convert.ToInt32(res[0]);

                    det.nNroCalendario = Convert.ToInt32(res[1]);

                    det.nNroCuota = Convert.ToInt32(res[2]);

                    det.Monto = Convert.ToDecimal(res[3]);

                    det.EsDeuda = Convert.ToBoolean(res[4]);

                    return det;
                }).ToList();

                Pago input = new Pago();

                input.CreadoPor = pago.Creador;
                input.Detalles = detalles;
                input.Producto = new Producto() { nValor = pago.ProductoID };
                input.Fondeador = new Fondeador() { FondeadorID = pago.FondeadorID };

                var res = await _PagoService.Save(input);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Listar todos las Pagos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("All")]
        public async Task<IEnumerable<Pago>> All()
        {
            try
            {
                var res = await _PagoService.All(new Pago());
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Listar todos las Pagos
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Find")]
        public async Task<Pago> Find([FromForm] int PagoID)
        {
            try
            {
                var res = await _PagoService.Find(new Pago() { PagoID = PagoID });
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Listar todos las Pagos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Delete")]
        public async Task<int> Delete(int PagoID)
        {
            try
            {
                var res = await _PagoService.Delete(new Pago() { PagoID = PagoID });
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Listar todos las Pagos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("FindDeuda")]
        public async Task<List<PagoDetalle>> FindDeuda(int nCodCred)
        {
            try
            {
                var res = await _PagoService.FindDeuda(nCodCred);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}