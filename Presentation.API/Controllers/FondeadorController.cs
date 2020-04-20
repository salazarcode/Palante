using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Contracts.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FondeadorController : ControllerBase
    {
        private readonly IFondeadorService _FondeadorService;

        public FondeadorController(IFondeadorService FondeadorService)
        {
            _FondeadorService = FondeadorService;
        }

        /// <summary>
        /// Lista las fondeadoras existentes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("All")]
        public async Task<IEnumerable<Fondeador>> All()
        {
            try
            {
                var res = await _FondeadorService.All();
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Lista las fondeadoras existentes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Find")]
        public async Task<Fondeador> Find(int FondeadorID)
        {
            try
            {
                var res = await _FondeadorService.Find(FondeadorID);
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Guarda un fondeador
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Save")]
        public async Task<int> Save([FromForm] Fondeador fondeador)
        {
            try
            {
                var res = await _FondeadorService.Save(fondeador);
                return res;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        /// <summary>
        /// Elimina un fondeador
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Delete")]
        public async Task<int> Delete(int FondeadorID)
        {
            try
            {
                var res = await _FondeadorService.Delete(FondeadorID);
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}