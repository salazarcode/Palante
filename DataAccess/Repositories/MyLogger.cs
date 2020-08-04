using DAL.Abstractions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Domain.Contracts.Repositories;

namespace DAL.Repositories
{
    public class MyLogger : Repository, IMyLogger
    {
        public MyLogger(IConfiguration configuration) : base(configuration)
        {
        }

        async Task IMyLogger.Log(string traza)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("@texto", traza);

                using (var conn = new SqlConnection(_connectionString))
                {
                    await conn.ExecuteAsync("dbo.CreateLog @texto", parameters, null, 120);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
