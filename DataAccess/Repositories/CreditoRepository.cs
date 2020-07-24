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
    public class CreditoRepository : SuperRepository, ICreditoRepository
    {
        public CreditoRepository(IConfiguration configuration) : base(configuration)
        {
        }

        async Task<List<Credito>> IRepository<Credito>.All(Paginacion paginacion)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Credito>> Cumplimiento(int FondeadorID, string creditos)
        {
            try
            {
                string query = "exec dbo.EvaluaCreditos @FondeadorID, @creditos";

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@FondeadorID", FondeadorID);
                param.Add("@creditos", creditos);

                var res = await Query<Credito>(query, param);

                return res.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //---------------------------------------
        Task<int> IRepository<Credito>.Save(Credito entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Credito>> Search(CreditoSearch search)
        {
            try
                {
                string q = "exec dbo.FindCredito @q, @fecha, @EnFondeador";

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@q", search.Query);
                param.Add("@fecha", search.Fecha);
                param.Add("@EnFondeador", search.EnFondeador);

                using var conn = new SqlConnection(_connectionString);
                var list = await conn.QueryAsync<Credito, Producto, Credito>(
                    q,
                    (credito, producto) =>
                    {
                        credito.Producto = producto;
                        return credito;
                    },
                    param,
                    splitOn: "nCodigo");
                var res = list.Distinct().ToList();

                //Si el producto es un derivado de motors se le pone la clasificacion base de YAPAMOTORS
                res.ForEach(ele => {
                    var n = ele.Producto.nValor;
                    if (n == 9 || n == 8 || n == 7 || n == 6 || n == 2)
                    {
                        ele.Producto.cNomCod = "YAPAMOTORS";
                        ele.Producto.nValor = 2;
                    }
                });

                return list.Distinct().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<int> Delete(Credito entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<Credito>> Find(Credito credito)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CreditoVO>> PorEstado(string EstadosConcatenadosComa)
        {
            try
            {
                string q = "exec dbo.GetCreditosPorEstado @EstadosConcatenadosComa";

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@EstadosConcatenadosComa", EstadosConcatenadosComa);

                using var conn = new SqlConnection(_connectionString);
                var list = await conn.QueryAsync<CreditoVO>(q,param);
                var res = list.Distinct().ToList();

                return list.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Credito>> PendientesPorAprobacion(int FondeadorID, DateTime desde, DateTime hasta)
        {
            try
            {
                string query = "exec dbo.PendientesPorAprobacion @FondeadorID, @desde, @hasta";

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@FondeadorID", FondeadorID);
                param.Add("@desde", desde);
                param.Add("@hasta", hasta);

                var res = await Query<Credito>(query, param);

                return res.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Credito>> DisponiblesPorFondeador(int FondeadorID, DateTime desde, DateTime hasta)
        {
            try
            {
                string query = "exec dbo.DisponiblesPorFondeador @FondeadorID, @desde, @hasta";

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@FondeadorID", FondeadorID);
                param.Add("@desde", desde);
                param.Add("@hasta", hasta);

                var res = await Query<Credito>(query, param);

                return res.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Credito>> CreditosFondeador(DateTime desde, DateTime hasta)
        {
            try
            {
                string query = "exec dbo.GetCreditosFondeador @desde, @hasta";

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@desde", desde);
                param.Add("@hasta", hasta);

                var res = await Query<Credito>(query, param);

                return res.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
