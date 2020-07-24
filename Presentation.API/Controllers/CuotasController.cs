using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using Domain.Contracts.Repositories;
using Domain.Entities;
using Domain.Contracts.Services;
using Domain.ValueObjects;
using Transversal.Util;
using AutoMapper;
using GestionCartera.API.ValueObjects;

namespace GestionCartera.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CuotasController : ControllerBase
    {
        private readonly ICuotaService _CuotaService;

        public CuotasController(ICuotaService CuotaService)
        {
            _CuotaService = CuotaService;
        }



        /// <summary>
        /// Lista de cutas PorEstado
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetCuotas")]
        public async Task<List<Cuota>> GetCuotas([FromForm] DateTime pagosDesde, [FromForm] DateTime pagosHasta, [FromForm] string nEstadoCuota)
        {
            try
            {
                var res = await _CuotaService.GetCuotas(pagosDesde, pagosHasta, nEstadoCuota);
                return res;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        /// <summary>
        /// Lista de cutas PorEstado
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetCuotasPorVencer")]
        public async Task<List<Cuota>> GetCuotasPorVencer([FromForm] DateTime pagosDesde, [FromForm] DateTime pagosHasta, [FromForm] string codigoFondeador)
        {
            try
            {
                var res = await _CuotaService.GetCuotasPorVencer(pagosDesde, pagosHasta, codigoFondeador);
                return res;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        /// <summary>
        /// GetCuotasFondeador
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetCuotasFondeador")]
        public async Task<List<Cuota>> GetCuotasFondeador([FromForm] string buscar)
        {
            try
            {
                var res = await _CuotaService.GetCuotasFondeador(buscar);
                return res;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
