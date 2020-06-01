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
    public class ProductoRepository : SuperRepository, IProductoRepository
    {
        public ProductoRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public Task<List<Producto>> Find(Producto query)
        {
            throw new NotImplementedException();
        }

        async Task<List<Producto>> IRepository<Producto>.All(Paginacion pag = null)
        {
            try
            {
                string query = @"
                    select * 
                    from catalogocodigos 
                    where ncodigo = 4029 and nvalor in(1,10,2) order by nValor asc
                ";
                var res = await Query<Producto>(query, null);

                return res.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        Task<int> IRepository<Producto>.Delete(Producto producto)
        {
            throw new NotImplementedException();
        }


        Task<int> IRepository<Producto>.Save(Producto entity)
        {
            throw new NotImplementedException();
        }
    }
}
