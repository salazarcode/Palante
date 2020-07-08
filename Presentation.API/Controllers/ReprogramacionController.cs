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
    public class ReprogramacionController : ControllerBase
    {
        private readonly IReprogramacionService _ReprogramacionService;

        public ReprogramacionController(IReprogramacionService ReprogramacionService)
        {
            _ReprogramacionService = ReprogramacionService;
        }

        /// <summary>
        /// Lista las Reprogramacion existentes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("All")]
        public async Task<IEnumerable<Reprogramacion>> All()
        {
            try
            {
                var res = await _ReprogramacionService.All();
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Lista las Reprogramacion existentes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Find")]
        public async Task<Reprogramacion> Find(int input)
        {
            try
            {
                var res = await _ReprogramacionService.Find(input);
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Guarda un Reprogramacion
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Save")]
        public async Task<int> Save([FromBody] Reprogramacion input)
        {
            try
            {
                var res = await _ReprogramacionService.Save(input);
                return res;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        /// <summary>
        /// Elimina un Reprogramacion
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Delete")]
        public async Task<int> Delete(int input)
        {
            try
            {
                var res = await _ReprogramacionService.Delete(input);
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}