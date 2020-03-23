using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Dapper;
using System.Data.SqlClient;
using DataAccess.Contracts;
using DataAccess.Entities;

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
        public IEnumerable<Prestamo> Get()
        {
            var res = _prestamoRepo.All();
            return res;
        }

        /// <summary>
        /// Encontrar un prestamo por ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Find")]
        public Prestamo Find([FromForm] int ID)
        {
            try
            {
                var res = _prestamoRepo.Find(ID);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
