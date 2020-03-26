using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Abstractions
{
    public abstract class SuperRepository : Repository
    {
        public SuperRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<List<Entity>> Query<Entity>(string sql, Dictionary<string, object> parameters = null)
        {
            try
            {
                var res = await _conn.QueryAsync<Entity>(sql, new DynamicParameters(parameters));
                return res.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> Execute(string sql, Dictionary<string, object> parameters = null)
        {
            try
            {
                var res = await _conn.ExecuteAsync(sql, new DynamicParameters(parameters));
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
