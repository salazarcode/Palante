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

        async Task<List<Cartera>> IRepository<Cartera>.All()
        {
            try
            {
                string query = "select * from ventacartera.Carteras c inner join ventacartera.Fondeadores f on c.FondeadorID = f.FondeadorID";

                using var conn = new SqlConnection(_connectionString);
                var res = await conn.QueryAsync<Cartera, Fondeador, Cartera>(
                    query, 
                    (c, f) => 
                    {
                        c.Fondeador = f;
                        return c;
                    },
                    splitOn: "FondeadorID"
                );

                return res.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        Task<int> IRepository<Cartera>.Delete(int ID)
        {
            throw new NotImplementedException();
        }

        async Task<Cartera> IRepository<Cartera>.Find(int CarteraID)
        {

            try
            {
                string query = @"
                    select c.*, cred.*, f.* 
                    from 
                        ventacartera.Carteras c 
                        inner join ventacartera.Fondeadores f on c.FondeadorID = f.FondeadorID
	                    left outer join ventacartera.CarteraCredito cc on c.CarteraId = cc.CarteraId
	                    left outer join Creditos cred on cred.nCodCred = cc.nCodCred	
                    where 
                        c.CarteraID = @CarteraID
                    ";

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@CarteraID", CarteraID);

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
                    splitOn: "nCodCred,FondeadorID");

                return list.Distinct().ToList().First();
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
                    string query = "exec [VentaCartera].[CrearCartera] @CreadoPor, @Fondeadora, @creditos";

                    Dictionary<string, object> param = new Dictionary<string, object>();
                    param.Add("@CreadoPor", CreadoPor);
                    param.Add("@Fondeadora", Fondeador);
                    param.Add("@creditos", creditos);

                    var res = await Execute(query, param);

                    return res;
                }
                else
                {
                    string query = @"
                            update 
                                VentaCartera.Carteras 
                            set 
                                FondeadorID = @FondeadorID,
                                Modificado = @Modificado, 
                            where 
                                CarteraID = @CarteraID";

                    Dictionary<string, object> param = new Dictionary<string, object>();
                    param.Add("@CarteraID", entity.CarteraID);
                    param.Add("@FondeadorID", entity.Fondeador.FondeadorID);
                    param.Add("@Modificado", DateTime.Now);

                    var res = await Execute(query, param);

                    return res;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<int> Add(int CarteraID, int CreditoID)
        {
            try
            {
                string query = "insert into VentaCartera.CarteraCredito values(@CarteraID, @CreditoID)";

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@CarteraID", CarteraID);
                param.Add("@CreditoID", CreditoID);

                var res = await Execute(query, param);

                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<int> Remove(int CarteraID, int CreditoID)
        {
            try
            {
                string query = "delete from VentaCartera.CarteraCredito where CarteraID = @CarteraID and nCodCred = @CreditoID)";

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@CarteraID", CarteraID);
                param.Add("@CreditoID", CreditoID);

                var res = await Execute(query, param);

                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


    }
}
