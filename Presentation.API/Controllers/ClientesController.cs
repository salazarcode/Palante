using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using Domain.Entities;
using Domain.Contracts.Repositories;
using Domain.Contracts.Services;

namespace Presentation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        /// <summary>
        /// Listar todos los clientes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("All")]
        public async Task<IEnumerable<Cliente>> All()
        {
            try
            {
                var res = await _clienteService.All();
                return res;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /*
        /// <summary>
        /// Listar un cliente con sus detalles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Find")]
        public async Task<Cliente> Find(int ID)
        {
            try
            {
                var res = await _clienteService.Find(ID);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        */
    }
}
