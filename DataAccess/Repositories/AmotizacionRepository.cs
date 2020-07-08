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
    public class ReprogramacionRepository : SuperRepository, IReprogramacionRepository
    {
        public ReprogramacionRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<List<Reprogramacion>> All(Paginacion paginacion = null)
        {
            try
            {
                string query = @"dbo.GetReprogramacion @ReprogramacionID";

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@ReprogramacionID", paginacion.ReprogramacionID);

                return await Query<Reprogramacion>(query, param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> Delete(Reprogramacion entity)
        {
            try
            {
                string query = @"delete from dbo.Reprogramaciones where ReprogramacionID = @ReprogramacionID";

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@ReprogramacionID", entity.ReprogramacionID);

                return await Execute(query, param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Reprogramacion>> Find(Reprogramacion input)
        {
            try
            {
                string query = @"dbo.GetReprogramacion @ReprogramacionID";

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@ReprogramacionID", input.ReprogramacionID);

                return await Query<Reprogramacion>(query, param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> Save(Reprogramacion entity)
        {
            try
            {
                string query = @"dbo.GuardarReprogramacion 
                                        @ReprogramacionID, 
                                        @Tasa,
                                        @SaldoCapital,
                                        @NuevoCapital,
                                        @UltimoVencimiento,
                                        @Hoy,
                                        @DiasTranscurridos,
                                        @Factor,
                                        @InteresesTranscurridos,
                                        @KI,
                                        @Amortizacion,
                                        @Capital,
                                        @nCodCred,
                                        @Total,
                                        @NroCalendarioCOF,
                                        @Confirmacion";

                Dictionary<string, object> param = new Dictionary<string, object>();

                param.Add("@ReprogramacionID", entity.ReprogramacionID);
                param.Add("@Tasa", entity.Tasa);
                param.Add("@SaldoCapital", entity.SaldoCapital);
                param.Add("@NuevoCapital", entity.NuevoCapital);
                param.Add("@UltimoVencimiento", entity.UltimoVencimiento);
                param.Add("@Hoy", entity.Hoy);
                param.Add("@DiasTranscurridos", entity.DiasTranscurridos);
                param.Add("@Factor", entity.Factor);
                param.Add("@InteresesTranscurridos", entity.InteresesTranscurridos);
                param.Add("@KI", entity.KI);
                param.Add("@Amortizacion", entity.Amortizacion);
                param.Add("@nCodCred", entity.nCodCred);
                param.Add("@Capital", entity.Capital);
                param.Add("@Total", entity.Total);
                param.Add("@NroCalendarioCOF", entity.NroCalendarioCOF);

                if(entity.Confirmacion != null)
                    param.Add("@Confirmacion", entity.Confirmacion);

                return await Execute(query, param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
