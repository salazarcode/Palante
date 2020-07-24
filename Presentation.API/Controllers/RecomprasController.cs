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
    public class RecomprasController : ControllerBase
    {
        private readonly IRecompraService _RecompraService;

        public RecomprasController(IRecompraService RecompraService)
        {
            _RecompraService = RecompraService;
        }

        /// <summary>
        /// Listar todos los Creditos
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Save")]
        public async Task<int> Save([FromForm] SaveRecompraVO intake)
        {
            try
            {
                Recompra res = new Recompra
                {
                    CreadoPor = intake.CreadoPor,
                    RecompraID = intake.RecompraID,
                    Fondeador = new Fondeador
                    {
                        FondeadorID = intake.FondeadorID
                    },
                    Producto = new Producto
                    {
                        nValor = intake.ProductoID
                    },
                    CreditosJoined = intake.Creditos,
                    FechaCalculo = intake.FechaCalculo
                };

                int id = await _RecompraService.Save(res);

                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Quitar Crédito de una Recompra
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Remove")]
        public async Task<int> Remove(int RecompraID, int CreditoID)
        {
            try
            {
                var res = await _RecompraService.Add(RecompraID, CreditoID);
                return res;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        /// <summary>
        /// Cerrar Recompra
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Cerrar")]
        public async Task<int> Cerrar([FromForm] int RecompraID, [FromForm] DateTime FechaCierre)
        {
            try
            {
                var res = await _RecompraService.Cerrar(RecompraID, FechaCierre);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Agregar Crédito
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Add")]
        public async Task<int> Add(int RecompraID, int CreditoID)
        {
            try
            {
                var res = await _RecompraService.Add(RecompraID, CreditoID);
                return res;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Listar todos las Recompras
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("All")]
        public async Task<IEnumerable<Recompra>> All()
        {
            try
            {
                var res = await _RecompraService.All();
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Eliminar Recompra
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Eliminar")]
        public async Task<int> Eliminar(int RecompraID)
        {
            try
            {
                var res = await _RecompraService.Delete(RecompraID);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Eliminar Recompra
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Find")]
        public async Task<Recompra> Find(int RecompraID)
        {
            try
            {
                var res = await _RecompraService.Find(RecompraID);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        /// Obtener Archivo Excel
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetExcel")]
        public async Task<FileStreamResult> GetCsv(int RecompraID)
        {
            try
            {
                var nombre = @"experimento.csv";

                var dir = @"C:\Users\USUARIO\Desktop\";

                var Recompras = await _RecompraService.All();

                FileGenerator.CSV<Recompra>(Recompras.ToList(), dir + nombre);

                FileStream fs1 = new FileStream(dir, FileMode.Open, FileAccess.Read);

                return File(fs1, "text/csv", nombre);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}