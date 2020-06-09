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
        public async Task<IEnumerable<Cronograma>> CronogramasPalante(int nCodCred)
        {
            var res = await _CronogramaService.GetCronogramasPalante(nCodCred);
            return res;
        }

        /// <summary>
        /// Listar todos los cronogramas palante-fondeador de un crédito
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("CronogramasFondeador")]
        public async Task<IEnumerable<Cronograma>> CronogramasFondeador(int nCodCred)
        {
            var res = await _CronogramaService.GetCronogramasFondeador(nCodCred);
            return res;
        }
    }
}
