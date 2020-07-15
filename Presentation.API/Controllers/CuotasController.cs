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

        private readonly IMapper _mapper;
        private readonly ICuotaService _CuotaService;

        public CuotasController(ICuotaService CuotaService, IMapper mapper)
        {
            _CuotaService = CuotaService;
            _mapper = mapper;
        }



        /// <summary>
        /// Lista de cutas PorEstado
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("PorEstados")]
        public async Task<List<Credito>> PorEstadosConContraparte([FromForm] string nEstadoCuota, [FromForm] string nEstado)
        {
            try
            {
                var res = await _CuotaService.PorEstadosConContraparte(nEstadoCuota, nEstado);
                return res;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
