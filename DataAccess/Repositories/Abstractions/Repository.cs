using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Dapper;
using System.Linq;

namespace DataAccess.Repositories.Abstractions
{
    public abstract class Repository
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public Repository(IConfiguration configuration)
        {
            _configuration = configuration;            
            
            var enviroment = _configuration.GetSection("enviroment").Value;

            _connectionString = _configuration.GetConnectionString(enviroment);
        }

        public SqlConnection GetConnection() {
            return new SqlConnection(_connectionString);
        }
    }
}
