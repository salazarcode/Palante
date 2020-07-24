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
    public class CarteraRepository : SuperRepository, ICarteraRepository
    {
        public CarteraRepository(IConfiguration configuration) : base(configuration)
        {
        }

        async Task<List<Cartera>> IRepository<Cartera>.All(Paginacion paginacion = null)
        {
            try
            {
                string query = @"dbo.GetCarteras @ProductoID";

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@ProductoID", paginacion.producto);


                using var conn = new SqlConnection(_connectionString);
                var list = await conn.QueryAsync<Cartera, Fondeador, Producto, Cartera>(
                    query,
                    (cartera, fondeador, producto) =>
                    {
                        cartera.Fondeador = fondeador;
                        cartera.Producto = producto;
                        return cartera;
                    },
                    param,
                    splitOn: "FondeadorID,nCodigo");

                return list.Distinct().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        async Task<int> IRepository<Cartera>.Delete(Cartera cartera)
        {
            try
            {
                string query = "exec [EliminarCartera] @CarteraID, @ProductoID";
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@CarteraID", cartera.CarteraID);
                param.Add("@ProductoID", cartera.ProductoID);

                var res = await Execute(query, param);

                return res;
            }
            catch (Exception)
            {

                throw;
            }
        }

        async Task<List<Cartera>> IRepository<Cartera>.Find(Cartera cartera)
        {

            try
            {
                string query = @"dbo.FindCartera @CarteraID, @ProductoID";

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@CarteraID", cartera.CarteraID);
                param.Add("@ProductoID", cartera.ProductoID);

                var orderDictionary = new Dictionary<int, Cartera>();


                using var conn = new SqlConnection(_connectionString);
                var list = await conn.QueryAsync<Cartera, Credito, Fondeador, Cartera>(
                    query,
                    (cartera, credito, fondeador) =>
                    {                        
                        Cartera carteraEntry;
                        
                        if (!orderDictionary.TryGetValue(cartera.CarteraID, out carteraEntry))
                        {
                            carteraEntry = cartera;
                            carteraEntry.Fondeador = fondeador;
                            carteraEntry.Creditos = new List<Credito>();
                            orderDictionary.Add(carteraEntry.CarteraID, carteraEntry);
                        }
                        
                        carteraEntry.Creditos.Add(credito);
                        return carteraEntry;
                    },
                    param,
                    splitOn: "creditoID,FondeadorID");

                return list.Distinct().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        async Task<int> IRepository<Cartera>.Save(Cartera entity)
        {
            try
            {
                var CreadoPor = entity.CreadoPor;
                var Fondeador = entity.Fondeador.FondeadorID;
                var creditos = String.Join(',',entity.Creditos.Select(x=>x.nCodCred.ToString()));

                if (entity.CarteraID == 0)
                {
                    string query = "exec [CrearCartera] @FondeadorID, @ProductoID, @CreadoPor, @creditos, @creado, @esrepro";

                    Dictionary<string, object> param = new Dictionary<string, object>();
                    param.Add("@FondeadorID", Fondeador);
                    param.Add("@ProductoID", entity.ProductoID);
                    param.Add("@CreadoPor", CreadoPor);
                    param.Add("@creditos", creditos);
                    param.Add("@creado", entity.Creado);
                    param.Add("@esrepro", entity.esrepro);

                    var res = await Query<int>(query, param);

                    return res.First();
                }
                else
                {
                    string query = @"exec EditarCartera @CarteraID, @ProductoID, @FondeadorID, @creditos, @creado, @esrepro";

                    Dictionary<string, object> param = new Dictionary<string, object>();
                    param.Add("@CarteraID", entity.CarteraID);
                    param.Add("@ProductoID", entity.ProductoID);
                    param.Add("@FondeadorID", entity.Fondeador.FondeadorID);
                    param.Add("@creditos", creditos);
                    param.Add("@creado", entity.Creado);
                    param.Add("@esrepro", entity.esrepro);

                    var res = await Execute(query, param);

                    return res;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        async Task<int> ICarteraRepository.Add(int CarteraID, int ProductoID, int CreditoID)
        {
            try
            {
                string query = "insert into CarteraCredito values(@CarteraID, @ProductoID, @CreditoID, 0)";

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@CarteraID", CarteraID);
                param.Add("@ProductoID", CarteraID);
                param.Add("@CreditoID", CreditoID);

                var res = await Execute(query, param);

                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        async Task<int> ICarteraRepository.Remove(int CarteraID, int ProductoID, int CreditoID)
        {
            try
            {
                string query = "delete from CarteraCredito where CarteraID = @CarteraID and ProductoID = @ProductoID and nCodCred = @CreditoID";

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@CarteraID", CarteraID);
                param.Add("@ProductoID", ProductoID);
                param.Add("@CreditoID", CreditoID);

                var res = await Execute(query, param);

                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        async Task<int> ICarteraRepository.Cerrar(int CarteraID, int ProductoID, DateTime FechaCierre)
        {
            try
            {
                string query = "exec CerrarCartera @CarteraID, @ProductoID, @FechaCierre";

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@CarteraID", CarteraID);
                param.Add("@ProductoID", ProductoID);
                param.Add("@FechaCierre", FechaCierre);

                var res = await Execute(query, param);

                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
