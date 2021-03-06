﻿using System;
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

namespace GestionCartera.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CronogramasController : ControllerBase
    {
        private readonly ICronogramaService _CronogramaService;

        public CronogramasController(ICronogramaService CronogramaService)
        {
            _CronogramaService = CronogramaService;
        }

        /// <summary>
        /// Listar todos los cronogramas palante-cliente de un crédito
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("CronogramasPalante")]
        public async Task<IEnumerable<Cronograma>> CronogramasPalante(string codigo, bool ConUltimoCalendario = false)
        {
            var res = await _CronogramaService.GetCronogramasPalante(codigo, ConUltimoCalendario);
            return res;
        }

        /// <summary>
        /// Listar todos los cronogramas palante-cliente de un crédito
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("CronogramasPalanteExcel")]
        public async Task<FileContentResult> CronogramasPalanteExcel(string codigo)
        {
            try
            {
                List<Cronograma> cronogramas = await _CronogramaService.GetCronogramasPalante(codigo, true);

                List<CuotaVO> lista = cronogramas[0].Cuotas.Select(x=> {
                    CuotaVO res = new CuotaVO();
                    res.CodigoCredito = x.nCodCred.ToString();
                    res.NumeroCuota = x.nNroCuota.ToString();
                    res.FechaPago = x.dFecVcto.ToString();
                    res.Amortizacion = x.nCapital.ToString();
                    res.Interes = x.nIntereses.ToString();
                    res.PeriodoGracia = x.nPerGracia.ToString();
                    res.Encaje = "0.00";
                    res.TotalCuota = x.nCuotaMensual.ToString();
                    return res;
                }).ToList();

                var array = FileGenerator.ExcelToByteArray<CuotaVO>(lista, "Cronograma");

                var nombre = "Cronograma.xlsx";

                return File(array, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombre);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Listar todos los cronogramas palante-fondeador de un crédito
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("CronogramasFondeador")]
        public async Task<IEnumerable<Cronograma>> CronogramasFondeador(string codigo)
        {
            var res = await _CronogramaService.GetCronogramasFondeador(codigo, false);
            return res;
        }
    }
}
