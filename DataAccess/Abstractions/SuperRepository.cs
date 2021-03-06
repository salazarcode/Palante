﻿using Microsoft.Extensions.Configuration;
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
                using (var conn = new SqlConnection(_connectionString))
                {
                    var res = await conn.QueryAsync<Entity>(sql, new DynamicParameters(parameters), null, 120);
                    return res.ToList();
                }
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
                using (var conn = new SqlConnection(_connectionString))
                {
                    var res = await conn.ExecuteAsync(sql, new DynamicParameters(parameters), null, 120);
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
