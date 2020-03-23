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
    public class ClientesController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepo;

        public ClientesController(IClienteRepository clienteRepo)
        {
            _clienteRepo = clienteRepo;
        }

        /// <summary>
        /// Listar todos los clientes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Get")]
        public IEnumerable<Cliente> Get()
        {
            var res = _clienteRepo.All();
            return res;
        }

        /// <summary>
        /// Encontrar un cliente por ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Find")]
        public Cliente Find([FromForm] int ID)
        {
            try
            {
                var res = _clienteRepo.Find(ID);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
