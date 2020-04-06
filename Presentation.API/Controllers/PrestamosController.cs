using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Dapper;
using System.Data.SqlClient;
using Domain.Contracts.Repositories;
using Domain.Entities;

namespace Presentation.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PrestamosController : ControllerBase
    {
        private readonly IPrestamoRepository _prestamoRepo;

        public PrestamosController(IPrestamoRepository prestamoRepo)
        {
            _prestamoRepo = prestamoRepo;
        }

        /// <summary>
        /// Listar todos los prestamos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Get")]
        public async Task<IEnumerable<Prestamo>> Get()
        {
            var res = await _prestamoRepo.All();
            return res;
        }

        /// <summary>
        /// Encontrar un prestamo por ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Find")]
        public async Task<Prestamo> Find([FromForm] int ID)
        {
            try
            {
                var res = await _prestamoRepo.Find(ID);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
