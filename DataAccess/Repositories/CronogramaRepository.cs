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
    public class CronogramaRepository : SuperRepository, ICronogramaRepository
    {
        public CronogramaRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<List<Cronograma>> GetCronogramasPalante(string codigo, bool ConUltimoCalendario)
        {
            try
            {
                string query = @"dbo.GetCronogramas 1,@codigo";

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@codigo", codigo);
                param.Add("@ultimo", ConUltimoCalendario);

                var cronogramaDictionary = new Dictionary<int, Cronograma>();

                using var conn = new SqlConnection(_connectionString);
                var list = await conn.QueryAsync<Cronograma, Cuota, Cronograma>(
                    query,
                    (cronograma, cuota) =>
                    {
                        Cronograma cronogramaEntry;

                        if (!cronogramaDictionary.TryGetValue(cronograma.nNroCalendario, out cronogramaEntry))
                        {
                            cronogramaEntry = cronograma;
                            cronogramaEntry.Cuotas = new List<Cuota>();
                            cronogramaDictionary.Add(cronogramaEntry.nNroCalendario, cronogramaEntry);
                        }

                        cronogramaEntry.Cuotas.Add(cuota);
                        return cronogramaEntry;
                    },
                    param,
                    splitOn: "CodigoCredito");

                return list.Distinct().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<Cronograma>> GetCronogramasFondeador(string codigo, bool ConUltimoCalendario)
        {
            try
            {
                string query = @"dbo.GetCronogramas 2,@codigo";

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@codigo", codigo);
                param.Add("@ultimo", ConUltimoCalendario);

                var cronogramaDictionary = new Dictionary<int, Cronograma>();

                using var conn = new SqlConnection(_connectionString);
                var list = await conn.QueryAsync<Cronograma, Cuota, Cronograma>(
                    query,
                    (cronograma, cuota) =>
                    {
                        Cronograma cronogramaEntry;

                        if (!cronogramaDictionary.TryGetValue(cronograma.nNroCalendario, out cronogramaEntry))
                        {
                            cronogramaEntry = cronograma;
                            cronogramaEntry.Cuotas = new List<Cuota>();
                            cronogramaDictionary.Add(cronogramaEntry.nNroCalendario, cronogramaEntry);
                        }

                        cronogramaEntry.Cuotas.Add(cuota);
                        return cronogramaEntry;
                    },
                    param,
                    splitOn: "CodigoCredito");

                return list.Distinct().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
