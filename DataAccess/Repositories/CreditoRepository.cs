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
            try
            {
                string sp = paginacion.repro ? "GetCreditosPaginadosRepro" : "GetCreditosPaginados";
                string query = "exec dbo." + sp + " @page, @pagesize, @producto";

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@page", paginacion.page);
                param.Add("@pagesize", paginacion.pagesize);
                param.Add("@producto", paginacion.producto);

                using var conn = new SqlConnection(_connectionString);
                var list = await conn.QueryAsync<Credito, Producto, Credito>(
                    query,
                    (credito, producto) =>
                    {
                        credito.Producto = producto;
                        return credito;
                    },
                    param,
                    splitOn: "nCodCred,nCodigo");
                var res = list.Distinct().ToList();

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
                string q = "exec dbo.FindCredito @tipo, @q, @fecha";

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@tipo", search.Tipo);
                param.Add("@q", search.Query);
                param.Add("@fecha", search.Fecha);

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
    }
}
