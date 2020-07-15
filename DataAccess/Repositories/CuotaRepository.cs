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

namespace DAL.Repositories
{
    public class CuotaRepository : SuperRepository, ICuotaRepository
    {
        public CuotaRepository(IConfiguration configuration) : base(configuration)
        {
        }
        public Task<List<Cuota>> All(Paginacion paginacion = null)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(Cuota entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<Cuota>> Find(Cuota param)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Credito>> PorEstadosConContraparte(string nEstadoCuota, string nEstado)
        {
            try
            {
                string q = "exec dbo.GetCuotasPorEstado @nEstadoCuota, @nEstado";

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@nEstadoCuota", nEstadoCuota);
                param.Add("@nEstado", nEstado);


                var dict = new Dictionary<int, Credito>();

                using var conn = new SqlConnection(_connectionString);
                var list = await conn.QueryAsync<Credito, Producto, Cuota, Credito>(
                    q,
                    (credito, producto, cuota) =>
                    {
                        Credito credFinal;                        
                        
                        if (!dict.TryGetValue(credito.nCodCred, out credFinal))
                        {
                            credFinal = credito;
                            credito.Producto = producto;
                            credito.CuotasVencidasVigentes = new List<Cuota>();
                            dict.Add(credito.nCodCred, credFinal);
                        }

                        credFinal.CuotasVencidasVigentes.Add(cuota);
                        return credFinal;
                    },
                    param,
                    splitOn: "nCodigo,codigo");
                var res = list.Distinct().ToList();

                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<int> Save(Cuota entity)
        {
            throw new NotImplementedException();
        }
    }
}
