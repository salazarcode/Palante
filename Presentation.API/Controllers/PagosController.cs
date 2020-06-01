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
    public class PagosController : ControllerBase
    {
        private readonly IPagoService _PagoService;

        public PagosController(IPagoService PagoService)
        {
            _PagoService = PagoService;
        }

            /*
        /// <summary>
        /// Listar todos los Cuotas
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Save")]
        public async Task<int> Save([FromForm] SavePagoVO intake)
        {
            try
            {
                Pago res = new Pago
                {
                    PagoID = intake.PagoID,
                    Fondeador = new Fondeador { 
                        FondeadorID = intake.FondeadorID
                    },
                    Cuotas = new List<Cuota>()
                };

                if (intake.Cuotas != "")
                    intake.Cuotas.Split(",").ToList().ForEach(x => res.Cuotas.Add(new Cuota { 
                        CuotaID = Convert.ToInt32(x)
                    }));

                int id = await _PagoService.Save(res);

                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
          
        }
  */
        /// <summary>
        /// Quitar Crédito de una Pago
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Remove")]
        public async Task<int> Remove(int PagoID, int nCodCred, int nNroCuota)
        {
            try
            {
                var res = await _PagoService.Add(PagoID, nCodCred, nNroCuota);
                return res;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    

    /// <summary>
    /// Listar todos los Creditos
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [Route("Save")]
    public async Task<int> Save([FromForm] int fondeadorID, [FromForm] string creadoPor)
    {
        try
        {
            int NewCarteraID = await _PagoService.Save(new Pago { 
                Fondeador = new Fondeador { FondeadorID = fondeadorID},
                CreadoPor = creadoPor
            });

            return NewCarteraID;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    /// <summary>
    /// Cerrar Pago
    /// </summary>
    /// <returns></returns>
    [HttpPost]
        [Route("Cerrar")]
        public async Task<int> Cerrar([FromForm] int PagoID, [FromForm] DateTime FechaCierre)
        {
            try
            {
                var res = await _PagoService.Cerrar(PagoID, FechaCierre);
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
        public async Task<int> Add(int PagoID, int nCodCred, int nNroCuota)
        {
            try
            {
                var res = await _PagoService.Add(PagoID, nCodCred, nNroCuota);
                return res;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Listar todos las Pagos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("All")]
        public async Task<IEnumerable<Pago>> All()
        {
            try
            {
                var res = await _PagoService.All();
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        /// Eliminar Pago
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Eliminar")]
        public async Task<int> Eliminar(int PagoID)
        {
            try
            {
                var res = await _PagoService.Delete(PagoID);
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
        public async Task<FileStreamResult> GetCsv(int PagoID)
        {
            try
            {
                var nombre = @"experimento.csv";

                var dir = @"C:\Users\USUARIO\Desktop\";

                var Pagos = await _PagoService.All();

                FileGenerator.CSV<Pago>(Pagos.ToList(), dir + nombre);

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