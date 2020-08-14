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
        public IMyLogger _logger { get; set; }
        public RecompraRepository(IConfiguration configuration, ICreditoRepository creditosRepository, IMyLogger logger) : base(configuration)
        {
            _creditosRepository = creditosRepository;
            _logger = logger;
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
                        select r.*,cr.* 
                        from dbo.recompras r
                        inner join dbo.CreditoRecompra cr on cr.RecompraID = r.RecompraID
                        order by r.recompraid desc
                    ";

                var orderDictionary = new Dictionary<int, Recompra>();


                using var conn = new SqlConnection(_connectionString);
                var list = await conn.QueryAsync<Recompra, RecompraDetalle, Recompra>(
                    query,
                    (Recompra, RecompraDetalle) =>
                    {
                        Recompra RecompraEntry;

                        if (!orderDictionary.TryGetValue(Recompra.RecompraID, out RecompraEntry))
                        {
                            RecompraEntry = Recompra;
                            RecompraEntry.Detalles = new List<RecompraDetalle>();
                            orderDictionary.Add(RecompraEntry.RecompraID, RecompraEntry);
                        }

                        RecompraEntry.Detalles.Add(RecompraDetalle);
                        return RecompraEntry;
                    },
                    splitOn: "codigoFondeador");

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
	                    r.*, cr.*
                    from 
                        Recompras r 
                        inner join creditorecompra cr on cr.recompraid = r.recompraid
                    where
                        r.recompraid =" + recompra.RecompraID;

                var orderDictionary = new Dictionary<int, Recompra>();


                using var conn = new SqlConnection(_connectionString);
                var list = await conn.QueryAsync<Recompra, RecompraDetalle, Recompra>(
                    query,
                    (Recompra, RecompraDetalle) =>
                    {
                        Recompra RecompraEntry;

                        if (!orderDictionary.TryGetValue(Recompra.RecompraID, out RecompraEntry))
                        {
                            RecompraEntry = Recompra;
                            RecompraEntry.Detalles = new List<RecompraDetalle>();
                            orderDictionary.Add(RecompraEntry.RecompraID, RecompraEntry);
                        }

                        RecompraEntry.Detalles.Add(RecompraDetalle);
                        return RecompraEntry;
                    },
                    splitOn: "codigoFondeador");


                var res = list.Distinct().ToList();

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
                if (entity.RecompraID == 0)
                {
                    string query = "exec [CrearRecompra] @CreadoPor, @FechaCalculo, @FondeadorID, @ProductoID";

                    Dictionary<string, object> param = new Dictionary<string, object>();
                    param.Add("@CreadoPor", entity.CreadoPor);
                    param.Add("@FechaCalculo", entity.FechaCalculo);
                    param.Add("@FondeadorID", entity.FondeadorID);
                    param.Add("@ProductoID", entity.ProductoID);

                    var RecompraID = (await Query<int>(query, param)).First();

                    string QueryDetalles = @"
                        exec RecompraAdd 
	                        @RecompraID,
	                        @codigoFondeador,
	                        @CapitalVigenteVencido,
	                        @GraciaVigenteVencido,
	                        @InteresVigenteVencido,
	                        @CapitalPorVencer,
	                        @Tasa,
	                        @CapitalTotal,
	                        @GraciaTotal,
	                        @InteresTotal,
	                        @PrecioRecompra
                    ";

                    List<int> resultados = new List<int>();

                    for (int i = 0; i < entity.Detalles.Count; i++)
                    {
                        var detalle = entity.Detalles[i];
                        try
                        {
                            Dictionary<string, object> paramx = new Dictionary<string, object>();
                            paramx.Add("@RecompraID", RecompraID);
                            paramx.Add("@codigoFondeador", detalle.codigoFondeador);

                            paramx.Add("@CapitalVigenteVencido",    detalle.CapitalVigenteVencido   );
                            paramx.Add("@GraciaVigenteVencido",     detalle.GraciaVigenteVencido    );
                            paramx.Add("@InteresVigenteVencido",    detalle.InteresVigenteVencido   );
                            paramx.Add("@CapitalPorVencer", detalle.CapitalPorVencer);
                            paramx.Add("@Tasa", detalle.Tasa);
                            paramx.Add("@CapitalTotal",             detalle.CapitalTotal            );
                            paramx.Add("@GraciaTotal",              detalle.GraciaTotal             );
                            paramx.Add("@InteresTotal",             detalle.InteresTotal            );
                            paramx.Add("@PrecioRecompra",           detalle.PrecioRecompra          );

                            var res = await Execute(QueryDetalles, paramx);
                            resultados.Add(res);
                        }
                        catch (Exception ex)
                        {
                            await _logger.Log("Error en la inserción de detalles en la recompra: RecompraID: " + RecompraID + ", codigoFondeador: " + detalle.codigoFondeador + ", Error: " + ex.Message);
                        }
                    }

                    return RecompraID;
                }
                else
                {
                    string query = @"exec EditarRecompra @RecompraID, @FondeadorID, @ProductoID, @creditos";

                    Dictionary<string, object> param = new Dictionary<string, object>();
                    param.Add("@RecompraID", entity.RecompraID);
                    param.Add("@CreadoPor", entity.CreadoPor);
                    param.Add("@FondeadorID", entity.FondeadorID);
                    param.Add("@ProductoID", entity.ProductoID);

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
