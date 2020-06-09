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

namespace GestionCartera.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        private readonly IReporteService _ReporteService;
        private static Random _random;
        private readonly string _dir;
        private readonly IConfiguration _conf;

        public ReportesController(IReporteService ReporteService, IConfiguration conf)
        {
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

        [HttpGet]
        [Route("GetZip")]
        public async Task<FileStreamResult> GetZip(int CarteraID, int ProductoID)
        {
            string dirbase = _dir;
            string dirname = GetRandom(10);

            string ruta = _dir + dirname;

            DirectoryInfo di = null;
            if (!Directory.Exists(ruta))
            {
                di = Directory.CreateDirectory(ruta);

                var nombre1 = ruta + "/" + "cliente.txt";
                var reporte1 = await _ReporteService.GetClientesCSV(CarteraID, ProductoID);
                FileGenerator.CSV<ClientesCSV>(reporte1, nombre1);

                var nombre2 = ruta + "/" + "cronogramas.txt";
                var reporte2 = await _ReporteService.GetCronogramasCSV(CarteraID, ProductoID);
                FileGenerator.CSV<CronogramasCSV>(reporte2, nombre2);

                var nombre3 = ruta + "/" + "creditos.txt";
                var reporte3 = await _ReporteService.GetCreditosCSV(CarteraID, ProductoID);
                FileGenerator.CSV<CreditosCSV>(reporte3, nombre3);

                var nombre4 = ruta + "/" + "clasificaciones.txt";
                var reporte4 = await _ReporteService.GetClasificacionesCSV(CarteraID, ProductoID);
                FileGenerator.CSV<ClasificacionesCSV>(reporte4, nombre4);

                var nombre5 = ruta + "/" + "Resumen.xlsx";
                var reporte5 = await _ReporteService.ResumenYapamotors(CarteraID, ProductoID);
                FileGenerator.Excel<ResumenYapamotors>(reporte5, "Resumen", nombre5);

                var nombre6 = ruta + "/" + "Anexo.xlsx";
                var reporte6 = await _ReporteService.AnexoYapamotors(CarteraID, ProductoID);
                FileGenerator.Excel<AnexoYapamotors>(reporte6, "Anexo", nombre6);


            }

            string zipDir = _dir + GetRandom(10);
            DirectoryInfo diZip = Directory.CreateDirectory(zipDir);

            string zipName = zipDir + "/comprimidos.zip";
            ZipFile.CreateFromDirectory(ruta, zipName);

            FileStream fs1 = new FileStream(zipName, FileMode.Open, FileAccess.Read);

            return File(fs1, "application/zip", "comprimidos.zip");
        }
    }
}