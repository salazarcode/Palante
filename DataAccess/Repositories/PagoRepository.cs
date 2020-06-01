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
    public class PagoRepository : SuperRepository, IPagoRepository
    {
        public PagoRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<List<Pago>> All(Paginacion pag = null)
        {
            try
            {
                string query = @"
                    select 
	                    p.*, cro.*
                    from 
                        pagos p 
                        inner join cuotapago cp on cp.pagoid = p.pagoid
                        inner join credcronograma cro on cro.ncodcred = cp.ncodcred and cro.nnrocuota = cp.nnrocuota
                    ";

                var orderDictionary = new Dictionary<int, Pago>();


                using var conn = new SqlConnection(_connectionString);
                var list = await conn.QueryAsync<Pago, Cuota, Pago>(
                    query,
                    (pago, cuota) =>
                    {
                        Pago pagoEntry;

                        if (!orderDictionary.TryGetValue(pago.PagoID, out pagoEntry))
                        {
                            pagoEntry = pago;
                            pagoEntry.Cuotas = new List<Cuota>();
                            orderDictionary.Add(pagoEntry.PagoID, pagoEntry);
                        }

                        pagoEntry.Cuotas.Add(cuota);
                        return pagoEntry;
                    },
                    splitOn: "nCodCred");

                return list.Distinct().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> Cerrar(int PagoID, DateTime FechaCierre)
        {
            try
            {
                string query = "exec CerrarPago @PagoID, @FechaCierre";

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@PagoID", PagoID);
                param.Add("@FechaCierre", FechaCierre);

                var res = await Execute(query, param);

                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> Delete(Pago pago)
        {
            try
            {
                string query = "exec [EliminarPago] @PagoID";
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@PagoID", pago.PagoID);

                var res = await Execute(query, param);

                return res;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<int> Add(int PagoID, int nCodCred, int nNroCuota)
        {
            try
            {
                string query = "insert into CuotaPago values(@PagoID, @nCodCred, @nNroCuota)";

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@PagoID", PagoID);
                param.Add("@nCodCred", nCodCred);
                param.Add("@nNroCuota", nNroCuota);

                var res = await Execute(query, param);

                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<int> Remove(int PagoID, int nCodCred, int nNroCuota)
        {
            try
            {
                string query = "delete from CuotaPago where PagoID = @PagoID and nCodCred = @nCodCred and nNroCuota = @nNroCuota)";

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@PagoID", PagoID);
                param.Add("@nCodCred", nCodCred);
                param.Add("@nNroCuota", nNroCuota);

                var res = await Execute(query, param);

                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<int> Save(Pago entity)
        {
            try
            {
                string query = "insert into pagos values(@FondeadorID, getdate(), null, null, @CreadoPor)";

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@FondeadorID", entity.Fondeador.FondeadorID);
                param.Add("@CreadoPor", entity.CreadoPor);

                var res = await Execute(query, param);

                return res;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Task<List<Pago>> Find(Pago query)
        {
            throw new NotImplementedException();
        }
    }
}
