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
    public class RecompraRepository : SuperRepository, IRecompraRepository
    {
        public RecompraRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<int> Add(int RecompraID, int nCodCred)
        {
            try
            {
                string query = "insert into CuotaRecompra values(@RecompraID, @nCodCred)";

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@RecompraID", RecompraID);
                param.Add("@nCodCred", nCodCred);

                var res = await Execute(query, param);

                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<Recompra>> All(Paginacion pag = null)
        {
            try
            {
                string query = @"
                    select 
	                    p.*, c.*
                    from 
                        ventacartera.Recompras p 
                        inner join creditos c on c.Recompraid = p.Recompraid
                    ";

                var orderDictionary = new Dictionary<int, Recompra>();


                using var conn = new SqlConnection(_connectionString);
                var list = await conn.QueryAsync<Recompra, Credito, Recompra>(
                    query,
                    (Recompra, credito) =>
                    {
                        Recompra RecompraEntry;

                        if (!orderDictionary.TryGetValue(Recompra.RecompraID, out RecompraEntry))
                        {
                            RecompraEntry = Recompra;
                            RecompraEntry.Creditos = new List<Credito>();
                            orderDictionary.Add(RecompraEntry.RecompraID, RecompraEntry);
                        }

                        RecompraEntry.Creditos.Add(credito);
                        return RecompraEntry;
                    },
                    splitOn: "RecompraID");

                return list.Distinct().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> Cerrar(int RecompraID, DateTime FechaCierre)
        {
            try
            {
                string query = "exec CerrarRecompra @RecompraID, @FechaCierre";

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@RecompraID", RecompraID);
                param.Add("@FechaCierre", FechaCierre);

                var res = await Execute(query, param);

                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> Delete(int RecompraID)
        {
            try
            {
                string query = "exec [EliminarRecompra] @RecompraID";
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@RecompraID", RecompraID);

                var res = await Execute(query, param);

                return res;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<int> Delete(Recompra entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<Recompra>> Find(Recompra recompra)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Remove(int RecompraID, int nCodCred)
        {
            try
            {
                string query = "delete from CuotaRecompra where RecompraID = @RecompraID and nCodCred = @nCodCred)";

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@RecompraID", RecompraID);
                param.Add("@nCodCred", nCodCred);

                var res = await Execute(query, param);

                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<int> Save(Recompra entity)
        {
            try
            {
                var CreadoPor = entity.CreadoPor;
                var Fondeador = entity.Fondeador.FondeadorID;
                var creditos = String.Join(',', entity.Creditos.Select(x => x.nCodCred.ToString()));

                if (entity.RecompraID == 0)
                {
                    string query = "exec [CrearRecompra] @CreadoPor, @Fondeadora, @creditos";

                    Dictionary<string, object> param = new Dictionary<string, object>();
                    param.Add("@CreadoPor", CreadoPor);
                    param.Add("@Fondeadora", Fondeador);
                    param.Add("@creditos", creditos);

                    var res = await Execute(query, param);

                    return res;
                }
                else
                {
                    string query = @"exec EditarRecompra @RecompraID, @Fondeador, @creditos";

                    Dictionary<string, object> param = new Dictionary<string, object>();
                    param.Add("@RecompraID", entity.RecompraID);
                    param.Add("@Fondeador", entity.Fondeador.FondeadorID);
                    param.Add("@creditos", creditos);

                    var res = await Execute(query, param);

                    return res;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
