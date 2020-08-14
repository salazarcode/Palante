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
    public class CreditosController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly ICreditoService _CreditoService;

        public CreditosController(ICreditoService CreditoService, IMapper mapper)
        {
            _CreditoService = CreditoService;
            _mapper = mapper;
        }

        /// <summary>
        /// Listar todos los Creditos
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("All")]
        public async Task<IEnumerable<Credito>> All([FromForm] Paginacion paginacion)
        {
            try
            {
                var res = await _CreditoService.All(paginacion);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Listar todos los Creditos
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("CreditosFondeador")]
        public async Task<IEnumerable<Credito>> CreditosFondeador([FromForm] DateTime desde, [FromForm] DateTime hasta, [FromForm] string busqueda = "")
        {
            try
            {
                var res = await _CreditoService.CreditosFondeador(desde, hasta, busqueda);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Listar todos los Creditos
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("PendientesPorAprobacion")]
        public async Task<FileContentResult> PendientesPorAprobacion([FromForm] int FondeadorID, [FromForm] DateTime desde, [FromForm] DateTime hasta)
        {
            try
            {
                var res = await _CreditoService.PendientesPorAprobacion(FondeadorID, desde, hasta);

                if (res.Count() == 0)
                    throw new Exception();

                List<CreditoDTO> creditosMapeados = _mapper.Map<List<Credito>, List<CreditoDTO>>(res);

                var array = FileGenerator.ExcelToByteArray<CreditoDTO>(creditosMapeados, "PendientesPorAprobacion");

                var nombre = "PendientesPorAprobacion.xlsx";

                return File(array, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombre);
            }
            catch (Exception ex)
            {
                ex.Data.Add("res", false);
                throw ex;
            }
        }

        /// <summary>
        /// DisponiblesPorFondeador
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("DisponiblesPorFondeador")]
        public async Task<FileContentResult> DisponiblesPorFondeador([FromForm] int FondeadorID, [FromForm] DateTime desde, [FromForm] DateTime hasta)
        {
            try
            {
                var res = await _CreditoService.DisponiblesPorFondeador(FondeadorID, desde, hasta);

                if (res.Count() == 0)
                    throw new Exception();


                List<CreditoDTO> creditosMapeados = _mapper.Map<List<Credito>, List<CreditoDTO>>(res);

                var array = FileGenerator.ExcelToByteArray<CreditoDTO>(creditosMapeados, "DisponiblesPorFondeador");

                var nombre = "DisponiblesPorFondeador.xlsx";

                return File(array, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombre);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Listar todos los Creditos
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Cumplimiento")]
        public async Task<IEnumerable<Credito>> Cumplimiento([FromForm] int FondeadorID, [FromForm] string creditos)
        {
            try
            {
                var res = await _CreditoService.Cumplimiento(FondeadorID, creditos);
                return res;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        /// <summary>
        /// Search
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Search")]
        public async Task<IEnumerable<Credito>> Search([FromForm] CreditoSearch param)
        {
            try
            {
                var res = await _CreditoService.Search(param);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Search
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("PorEstado")]
        public async Task<FileContentResult> PorEstado([FromForm] string Estados)
        {
            try
            {            
                var res = await _CreditoService.PorEstado(Estados);

                var array = FileGenerator.ExcelToByteArray<CreditoVO>(res, "ReporteDeuda");

                var nombre = "CreditosPorEstado.xlsx";

                return File(array, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombre);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
