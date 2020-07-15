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
    public class RecompraRepository : SuperRepository, IRecompraRepository
    {
        public ICreditoRepository _creditosRepository { get; set; }
        public RecompraRepository(IConfiguration configuration, ICreditoRepository creditosRepository) : base(configuration)
        {
            _creditosRepository = creditosRepository;
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
	                    r.*, c.*
                    from 
                        Recompras r 
                        inner join creditorecompra cr on cr.recompraid = r.recompraid
                        inner join creditos c on c.ncodcred = cr.ncodcred
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
                    splitOn: "nCodCred");

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

        public async Task<int> Delete(Recompra entity)
        {
            try
            {
                string query = "exec [EliminarRecompra] @RecompraID";
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@RecompraID", entity.RecompraID);

                var res = await Execute(query, param);

                return res;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Recompra>> Find(Recompra recompra)
        {
            try
            {
                string query = @"
                    select 
	                    r.*, c.*
                    from 
                        Recompras r 
                        inner join creditorecompra cr on cr.recompraid = r.recompraid
                        inner join creditos c on c.ncodcred = cr.ncodcred
                    where
                        r.recompraid =" + recompra.RecompraID;

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
                    splitOn: "nCodCred");


                var res = list.Distinct().ToList();

                var creditosStr = String.Join(",", res.First().Creditos.Select(x => x.nCodCred));
                var creditosCompletos = await _creditosRepository.Search(new CreditoSearch() { 
                    Query = creditosStr,
                    Fecha = DateTime.Now
                });

                res.First().Creditos = creditosCompletos;

                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                var creditos = String.Join(',', entity.Creditos.Select(x => x.nCodCred.ToString()));

                if (entity.RecompraID == 0)
                {
                    string query = "exec [CrearRecompra] @CreadoPor, @FondeadorID, @ProductoID, @creditos";

                    Dictionary<string, object> param = new Dictionary<string, object>();
                    param.Add("@CreadoPor", entity.CreadoPor);
                    param.Add("@FondeadorID", entity.Fondeador.FondeadorID);
                    param.Add("@ProductoID", entity.Producto.nValor);
                    param.Add("@creditos", creditos);

                    var res = await Execute(query, param);

                    return res;
                }
                else
                {
                    string query = @"exec EditarRecompra @RecompraID, @FondeadorID, @ProductoID, @creditos";

                    Dictionary<string, object> param = new Dictionary<string, object>();
                    param.Add("@RecompraID", entity.RecompraID);
                    param.Add("@CreadoPor", entity.CreadoPor);
                    param.Add("@FondeadorID", entity.Fondeador.FondeadorID);
                    param.Add("@ProductoID", entity.Producto.nValor);
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
