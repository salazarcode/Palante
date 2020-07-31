using Microsoft.Extensions.Configuration;
using DAL.Abstractions;
using Domain.Contracts.Repositories;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain.Entities;
using System;
using System.Linq;
using Dapper;
using Dapper.Mapper;
using System.Data.SqlClient;
using Domain.ValueObjects;

namespace DAL.Repositories
{
    public class ReporteRepository : SuperRepository, IReporteRepository
    {
        public ReporteRepository(IConfiguration configuration) : base(configuration)
        {

        }

        async Task<List<ClasificacionesCSV>> IReporteRepository.GetClasificacionesCSV(int carteraid, int ProductoID)
        {

            try
            {
                string query = "exec dbo.CalificacionesCSV @carteraid, @ProductoID";
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@carteraid", carteraid);
                param.Add("@ProductoID", ProductoID);
                var res = await Query<ClasificacionesCSV>(query, param);

                return res.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        async Task<List<ResumenYapamotors>> IReporteRepository.ResumenYapamotors(int carteraid, int ProductoID)
        {

            try
            {
                string query = "exec dbo.ResumenYapamotors @carteraid, @ProductoID";
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@carteraid", carteraid);
                param.Add("@ProductoID", ProductoID);
                var res = await Query<ResumenYapamotors>(query, param);

                return res.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        async Task<List<ResumenPapyme>> IReporteRepository.ResumenPapyme(int carteraid, int ProductoID)
        {

            try
            {
                string query = "exec dbo.ResumenPapyme @carteraid, @ProductoID";
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@carteraid", carteraid);
                param.Add("@ProductoID", ProductoID);
                var registros = await Query<ResumenPapyme>(query, param);

                var operaciones = registros.Select(x => x.NroOperacion).Distinct().ToList();

                operaciones.ForEach(operacion =>
                {
                    var elems = registros.FindAll(x => x.NroOperacion == operacion);
                    if (elems.Count != 1) {
                        var elem = elems.First();

                        ResumenPapyme res = new ResumenPapyme();
                        res.ID                              = elem.ID;
                        res.NroOperacion                    = elem.NroOperacion;
                        res.DniClienteRepresentanteLegal    = elem.DniClienteRepresentanteLegal;
                        res.ApellidoPaterno                 = elem.ApellidoPaterno;
                        res.ApellidoMaterno                 = elem.ApellidoMaterno;
                        res.Nombre                          = elem.Nombre;
                        res.Fondeador                       = elem.Fondeador;
                        res.NumeroVenta                     = elem.NumeroVenta;
                        res.FechaVenta                      = elem.FechaVenta;
                        res.RUC                             = elem.RUC;
                        res.RazonSocial                     = elem.RazonSocial;
                        res.FechaDesembolso                 = elem.FechaDesembolso;
                        res.MontoAprobadoSoles              = elem.MontoAprobadoSoles;

                        res.TipoVivienda = elems.Count().ToString() + " GARANTÍA" + (elems.Count() > 1 ? "S" : "");
                        res.ValorComercial = elems.Select(x => Convert.ToDecimal(x.ValorComercial)).Sum().ToString();
                        res.ValorRealizacion = elems.Select(x => Convert.ToDecimal(x.ValorRealizacion)).Sum().ToString();

                        elems.ForEach(a =>
                        {
                            a.MontoAprobadoSoles = "";
                        });

                        registros.Add(res);                    
                    }
                });

                List<ResumenPapyme> resumenes = new List<ResumenPapyme>();
                operaciones.ForEach(operacion =>
                {
                    var res = registros.FindAll(x => x.NroOperacion == operacion);

                    registros.FindAll(x => x.NroOperacion == operacion && x.MontoAprobadoSoles != "").ForEach(ele => {
                        resumenes.Add(ele);
                    });

                    registros.FindAll(x => x.NroOperacion == operacion && x.MontoAprobadoSoles == "").ForEach(ele => {
                        resumenes.Add(ele);
                    });
                });

                for (int i = 1; i < resumenes.Count; i++)
                    resumenes[i].ID = i;

                return resumenes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        async Task<List<AnexoYapamotors>> IReporteRepository.AnexoYapamotors(int carteraid, int ProductoID)
        {

            try
            {
                string query = "exec dbo.AnexoYapamotors @carteraid, @ProductoID";
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@carteraid", carteraid);
                param.Add("@ProductoID", ProductoID);
                var res = await Query<AnexoYapamotors>(query, param);

                return res.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        async Task<List<AnexoPapyme>> IReporteRepository.AnexoPapyme(int carteraid, int ProductoID)
        {

            try
            {
                string query = "exec dbo.AnexoPapyme @carteraid, @ProductoID";
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@carteraid", carteraid);
                param.Add("@ProductoID", ProductoID);
                var res = await Query<AnexoPapyme>(query, param);

                return res.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        async Task<List<ClientesCSV>> IReporteRepository.GetClientesCSV(int carteraid, int ProductoID)
        {
            try
            {
                string query = "exec dbo.clientescsv @carteraid, @ProductoID";
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@carteraid", carteraid);
                param.Add("@ProductoID", ProductoID);
                var res = await Query<ClientesCSV>(query, param);

                return res.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        async Task<List<CreditosCSV>> IReporteRepository.GetCreditosCSV(int carteraid, int ProductoID)
        {
            try
            {
                string query = "exec dbo.creditoscsv @carteraid, @ProductoID";
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@carteraid", carteraid);
                param.Add("@ProductoID", ProductoID);
                var res = await Query<CreditosCSV>(query, param);

                return res.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        async Task<List<CronogramasCSV>> IReporteRepository.GetCronogramasCSV(int carteraid, int ProductoID)
        {
            try
            {
                string query = "exec dbo.cronogramascsv @carteraid, @ProductoID";
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@carteraid", carteraid);
                param.Add("@ProductoID", ProductoID);
                var res = await Query<CronogramasCSV>(query, param);

                return res.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<PagosExcel>> GetPagosExcel(int PagoID)
        {
            try
            {
                string query = "exec dbo.PagosExcel @PagoID";
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@PagoID", PagoID);

                var res = await Query<PagosExcel>(query, param);

                return res.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<PagosCSV>> GetPagosCSV(int PagoID)
        {
            try
            {
                string query = "exec dbo.PagosCSV @PagoID";
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@PagoID", PagoID);

                var res = await Query<PagosCSV>(query, param);

                return res.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
