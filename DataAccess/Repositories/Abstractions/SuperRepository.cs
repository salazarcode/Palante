using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using System.Linq;

namespace DataAccess.Repositories.Abstractions
{
    public abstract class SuperRepository : Repository
    {
        public SuperRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public List<Entity> Query<Entity>(string sql, Dictionary<string, object> parameters = null)
        {
            try
            {
                using var connection = GetConnection();
                var res = connection.Query<Entity>(sql, new DynamicParameters(parameters)).ToList();
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int Execute(string sql, Dictionary<string, object> parameters = null)
        {
            try
            {
                using var connection = GetConnection();
                var res = connection.Execute(sql, new DynamicParameters(parameters));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
