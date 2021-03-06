﻿using System;
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
using Domain.Contracts.Repositories;
using Microsoft.Extensions.Logging;

namespace GestionCartera.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PagosController : ControllerBase
    {
        private readonly IPagoService _PagoService;
        private readonly IMyLogger _logger;

        public PagosController(IPagoService PagoService, IMyLogger logger)
        {
            _PagoService = PagoService;
            _logger = logger;
        }

    

        /// <summary>
        /// Listar todos los Creditos
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Save")]
        public async Task<int> Save([FromBody] SavePagoVO pago)
        {
            try
            {
                List<PagoDetalle> detalles = pago.Pagos.Select(x =>
                {
                    PagoDetalle det = new PagoDetalle();
                    det.codigoFondeador = x.codigoFondeador;
                    det.nNroCuota = x.nNroCuota;
                    det.Monto = x.Monto;
                    det.EsDeuda = false;
                    return det;
                }).ToList();

                Pago input = new Pago();

                input.CreadoPor = pago.Creador;
                input.Detalles = detalles;
                input.Producto = new Producto() { nValor = pago.ProductoID };
                input.Fondeador = new Fondeador() { FondeadorID = pago.FondeadorID };
                input.EsMochila = pago.EsMochila;
                input.PagoID = pago.PagoID;

                var res = await _PagoService.Save(input);
                return res;
            }
            catch (Exception ex)
            {

                await _logger.Log("Error en Nuevo pago: " + ex.Message + ", Clase: " + this.GetType().FullName);
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
        /// Confirmar Pagos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Confirmar")]
        public async Task<int> Confirmar(int PagoID)
        {
            try
            {
                var res = await _PagoService.Confirmar(PagoID);
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