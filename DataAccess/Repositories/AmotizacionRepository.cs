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
    public class AmotizacionRepository : SuperRepository, IAmortizacionRepository
    {
        public AmotizacionRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<List<Amortizacion>> All(Paginacion paginacion = null)
        {
            try
            {
                string query = @"dbo.GetAmortizacion @AmortizacionID";

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@AmortizacionID", paginacion.AmortizacionID);

                return await Query<Amortizacion>(query, param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> Delete(Amortizacion entity)
        {
            try
            {
                string query = @"delete from dbo.Amortizaciones where AmortizacionID = @AmortizacionID";

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@AmortizacionID", entity.AmortizacionID);

                return await Execute(query, param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Amortizacion>> Find(Amortizacion input)
        {
            try
            {
                string query = @"dbo.GetAmortizacion @AmortizacionID";

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@AmortizacionID", input.AmortizacionID);

                return await Query<Amortizacion>(query, param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> Save(Amortizacion entity)
        {
            try
            {
                string query = @"dbo.GuardarAmortizacion 
                                        @AmortizacionID, 
                                        @Tasa,
                                        @SaldoCapital,
                                        @NuevoCapital,
                                        @UltimoVencimiento,
                                        @Hoy,
                                        @DiasTranscurridos,
                                        @Factor,
                                        @InteresesTranscurridos,
                                        @KI,
                                        @nAmortizacion,
                                        @Capital,
                                        @nCodCred,
                                        @Total,
                                        @NroCalendarioCOF,
                                        @Confirmacion";

                Dictionary<string, object> param = new Dictionary<string, object>();

                param.Add("@ReprogramacionID", entity.AmortizacionID);
                param.Add("@Tasa", entity.Tasa);
                param.Add("@SaldoCapital", entity.SaldoCapital);
                param.Add("@NuevoCapital", entity.NuevoCapital);
                param.Add("@UltimoVencimiento", entity.UltimoVencimiento);
                param.Add("@Hoy", entity.Hoy);
                param.Add("@DiasTranscurridos", entity.DiasTranscurridos);
                param.Add("@Factor", entity.Factor);
                param.Add("@InteresesTranscurridos", entity.InteresesTranscurridos);
                param.Add("@KI", entity.KI);
                param.Add("@nAmortizacion", entity.nAmortizacion);
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
