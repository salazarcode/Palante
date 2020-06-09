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
    [Route("[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _UsuarioService;

        public UsuariosController(IUsuarioService UsuarioService)
        {
            _UsuarioService = UsuarioService;
        }

        /// <summary>
        /// Detalle de Cartera
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Login")]
        public async Task<dynamic> Login([FromForm] string nombre, [FromForm] string clave)
        {
            try
            {
                Usuario res = await _UsuarioService.Login(new Usuario { Nombre = nombre, Clave = clave });
                return new
                {
                    codigo = res != null ? 1 : -1,
                    mensaje = res != null ? "" : "Clave o usuario equivocado",
                    data = res
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Cerrar sesión
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Logout")]
        public async Task<dynamic> Logout([FromForm] string token)
        {
            try
            {
                var res = await _UsuarioService.Logout(token);
                return new
                {
                    codigo = res != 0 ? 1 : -1,
                    mensaje = res != 0 ? "" : "Error",
                    data = res
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}