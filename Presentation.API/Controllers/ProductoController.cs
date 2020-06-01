using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GestionCartera.API.ValueObjects;
using Transversal.Util;

namespace GestionCartera.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _ProductoService;

        public ProductoController(IProductoService ProductoService)
        {
            _ProductoService = ProductoService;
        }

        /// <summary>
        /// Productos existentes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("All")]
        public async Task<List<Producto>> All()
        {
            try
            {
                List<Producto> res = await _ProductoService.All();
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}