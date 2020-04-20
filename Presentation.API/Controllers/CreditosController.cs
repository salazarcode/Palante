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

namespace Presentation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        [HttpGet]
        [Route("All")]
        public async Task<IEnumerable<Credito>> All()
        {
            var res = await _CreditoService.All();
            return res;
        }

        /// <summary>
        /// Listar todos los Creditos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Cumplimiento")]
        public async Task<IEnumerable<Credito>> Cumplimiento(int FondeadorID)
        {
            var res = await _CreditoService.Cumplimiento(FondeadorID);
            return res;
        }
    }
}
