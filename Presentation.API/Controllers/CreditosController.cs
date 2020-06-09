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
    public class CreditosController : ControllerBase
    {
        private readonly ICreditoService _CreditoService;

        public CreditosController(ICreditoService CreditoService)
        {
            _CreditoService = CreditoService;
        }

        /// <summary>
        /// Listar todos los Creditos
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("All")]
        public async Task<IEnumerable<Credito>> All([FromForm] Paginacion paginacion)
        {
            var res = await _CreditoService.All(paginacion);
            return res;
        }

        /// <summary>
        /// Listar todos los Creditos
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Cumplimiento")]
        public async Task<IEnumerable<Credito>> Cumplimiento([FromForm] int FondeadorID, [FromForm] string creditos)
        {
            var res = await _CreditoService.Cumplimiento(FondeadorID, creditos);
            return res;
        }

        /// <summary>
        /// Search
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Search")]
        public async Task<IEnumerable<Credito>> Search([FromForm] CreditoSearch param)
        {
            var res = await _CreditoService.Search(param);
            return res;
        }
    }
}
