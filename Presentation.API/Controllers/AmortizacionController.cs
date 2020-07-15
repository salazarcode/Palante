using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Contracts.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionCartera.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AmortizacionController : ControllerBase
    {
        private readonly IAmortizacionService _AmortizacionService;

        public AmortizacionController(IAmortizacionService AmortizacionService)
        {
            _AmortizacionService = AmortizacionService;
        }

        /// <summary>
        /// Lista las Amortizacion existentes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("All")]
        public async Task<IEnumerable<Amortizacion>> All()
        {
            try
            {
                var res = await _AmortizacionService.All();
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Lista las Amortizacion existentes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Find")]
        public async Task<Amortizacion> Find(int input)
        {
            try
            {
                var res = await _AmortizacionService.Find(input);
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Guarda un Amortizacion
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Save")]
        public async Task<int> Save([FromBody] Amortizacion input)
        {
            try
            {
                var res = await _AmortizacionService.Save(input);
                return res;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        /// <summary>
        /// Elimina un Amortizacion
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Delete")]
        public async Task<int> Delete(int input)
        {
            try
            {
                var res = await _AmortizacionService.Delete(input);
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Cerrar una Amortizacion
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Cerrar")]
        public async Task<int> Cerrar(int AmortizacionID)
        {
            try
            {
                var res = await _AmortizacionService.Cerrar(AmortizacionID);
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}