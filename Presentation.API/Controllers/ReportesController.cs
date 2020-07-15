using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts.Services;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GestionCartera.API.ValueObjects;
using Transversal.Util;
using Microsoft.Extensions.Configuration;
using Microsoft.CodeAnalysis;

namespace GestionCartera.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        private readonly IPagoService _PagoService;
        private readonly IReporteService _ReporteService;
        private readonly ICreditoService _CreditoService;
        private static Random _random;
        private readonly string _dir;
        private readonly IConfiguration _conf;

        public ReportesController(IReporteService ReporteService, ICreditoService CreditoService, IPagoService PagoService, IConfiguration conf)
        {
            _PagoService = PagoService;
            _CreditoService = CreditoService;
            _ReporteService = ReporteService;
            _random = new Random();

            _conf = conf;
            _dir = _conf.GetSection("dir").Value;
        }

        public static string GetRandom(int length) {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        [HttpPost]
        [Route("ReporteDeuda")]
        public async Task<FileContentResult> ReporteDeuda([FromForm] string creditos, [FromForm] DateTime fecha)
        {
            try
            {
                CreditoSearch parametros = new CreditoSearch();
                parametros.Query = creditos;
                parametros.Fecha = fecha;

                List<Credito> resCreditos = await _CreditoService.Search(parametros);

                List<ReporteDeuda> reportes = new List<ReporteDeuda>();
                
                resCreditos.ForEach(x =>
                {
                    var reporte = new ReporteDeuda();
                    reporte.nCodCred = x.nCodCred;
                    reporte.nMoneda = x.nMoneda;
                    reporte.nPrestamo = Convert.ToDecimal(x.nPrestamo);
                    reporte.dFecIni = x.dFecIni;
                    reporte.nEstado = x.nEstado;
                    reporte.nNroCuotas = x.nNroCuotas;
                    reporte.dni = x.dni;
                    reporte.nombres = x.nombres;
                    reporte.ruc = x.ruc;
                    reporte.razonSocial = x.razonSocial;
                    reporte.nombreProducto = x.nombreProducto;
                    reporte.precio = Convert.ToDecimal(x.precio);
                    reporte.deuda = x.deuda;
                    reportes.Add(reporte);
                });

                var array = FileGenerator.ExcelToByteArray<ReporteDeuda>(reportes, "ReporteDeuda");

                return File(array, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ReporteDeuda.xlsx");                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetZip")]
        public async Task<FileStreamResult> GetZip(int CarteraID, int ProductoID)
        {
            string dirname = GetRandom(10);

            string ruta = _dir + dirname;

            if (!Directory.Exists(ruta))
            {
                Directory.CreateDirectory(ruta);

                var absClientes = ruta + "/" + "cliente.txt";
                var clientes = await _ReporteService.GetClientesCSV(CarteraID, ProductoID);
                FileGenerator.CSV<ClientesCSV>(clientes, absClientes);

                var absCronogramas = ruta + "/" + "cronogramas.txt";
                var cronogramas = await _ReporteService.GetCronogramasCSV(CarteraID, ProductoID);
                FileGenerator.CSV<CronogramasCSV>(cronogramas, absCronogramas);

                var absCreditos = ruta + "/" + "creditos.txt";
                var creditos = await _ReporteService.GetCreditosCSV(CarteraID, ProductoID);
                FileGenerator.CSV<CreditosCSV>(creditos, absCreditos);

                var absClasificaciones = ruta + "/" + "clasificaciones.txt";
                var clasificaciones = await _ReporteService.GetClasificacionesCSV(CarteraID, ProductoID);
                FileGenerator.CSV<ClasificacionesCSV>(clasificaciones, absClasificaciones);

                var absResumen = ruta + "/" + "Resumen.xlsx";
                var resumen = await _ReporteService.ResumenYapamotors(CarteraID, ProductoID);
                FileGenerator.Excel<ResumenYapamotors>(resumen, "Resumen", absResumen);

                var absAnexo = ruta + "/" + "Anexo.xlsx";
                var anexo = await _ReporteService.AnexoYapamotors(CarteraID, ProductoID);
                FileGenerator.Excel<AnexoYapamotors>(anexo, "Anexo", absAnexo);

                var absSuperResumen = ruta + "/" + "ResumenDetallado.xlsx";
                Dictionary<string, dynamic> dict = new Dictionary<string, dynamic>();
                dict.Add("Clientes", clientes);
                dict.Add("Cronogramas", cronogramas);
                dict.Add("Clasificaciones", clasificaciones);
                dict.Add("Creditos", creditos);
                
                Dictionary<string, Type> dictTypes = new Dictionary<string, Type>();
                dictTypes.Add("Clientes", typeof(ClientesCSV));
                dictTypes.Add("Cronogramas", typeof(CronogramasCSV));
                dictTypes.Add("Clasificaciones", typeof(ClasificacionesCSV));
                dictTypes.Add("Creditos", typeof(CreditosCSV));
                FileGenerator.ExcelReporteVariasPaginas(dict, dictTypes, absSuperResumen);
            }

            string zipDir = _dir + GetRandom(10);
            DirectoryInfo diZip = Directory.CreateDirectory(zipDir);

            string zipName = zipDir + "/comprimidos.zip";
            ZipFile.CreateFromDirectory(ruta, zipName);

            FileStream fs1 = new FileStream(zipName, FileMode.Open, FileAccess.Read);

            return File(fs1, "application/zip", "comprimidos.zip");
        }

        [HttpGet]
        [Route("GetPago")]
        public async Task<FileStreamResult> GetPago(int PagoID)
        {
            string dirname = GetRandom(10);

            string ruta = _dir + dirname;

            if (!Directory.Exists(ruta))
            {
                Directory.CreateDirectory(ruta);

                var pagosCSV = await _ReporteService.GetPagosCSV(PagoID);
                var pagosExcel = await _ReporteService.GetPagosExcel(PagoID);

                var absPagosCSV = ruta + "/" + "pago.txt";
                FileGenerator.CSV<PagosCSV>(pagosCSV, absPagosCSV);

                var absPagoExcel = ruta + "/" + "pago.xlsx";
                FileGenerator.Excel<PagosExcel>(pagosExcel, "Pago" + PagoID, absPagoExcel);
            }

            string zipDir = _dir + GetRandom(10);
            DirectoryInfo diZip = Directory.CreateDirectory(zipDir);

            string zipName = zipDir + "/res.zip";
            ZipFile.CreateFromDirectory(ruta, zipName);

            FileStream fs1 = new FileStream(zipName, FileMode.Open, FileAccess.Read);

            return File(fs1, "application/zip", "comprimidos.zip");
        }
    }
}