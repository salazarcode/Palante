using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DAL.Abstractions;
using System.Threading.Tasks;
using Domain.Contracts.Repositories;
using Domain.Entities;

namespace DAL.Repositories
{
    public class ClienteRepository : SuperRepository, IClienteRepository
    {
        private readonly IPrestamoRepository _prestamoRepo;
        public ClienteRepository(IConfiguration configuration, IPrestamoRepository prestamoRepo) : base(configuration)
        {
            _prestamoRepo = prestamoRepo;
        }

        async Task<int> IRepository<Cliente>.Delete(int ID)
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@ID", ID);
                var res = await Execute("delete from cliente where ID = @ID", param);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        async Task<List<Cliente>> IRepository<Cliente>.Get(int ID = 0)
        {
            try
            {
                Dictionary<string, object> param = null;
                if (ID != 0)
                {
                    param = new Dictionary<string, object>();
                    param.Add("@ID", ID);
                }

                string query = "select * from cliente" + (ID != 0 ? " where ID = @ID" : "");

                var res = await Query<Cliente>(query, param);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        async Task<int> IRepository<Cliente>.Save(Cliente entity)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();

            try
            {
                if (entity.ID != 0)
                    param.Add("@ID", entity.ID);

                param.Add("@Nombres", entity.Nombres);

                string queryInsert = "insert into cliente values(@Nombres); select @@identity;";
                string queryUpdate = "update cliente set nombres = @Nombres where ID = @ID; select @@identity;";

                var res = await Execute(entity.ID != 0 ? queryUpdate : queryInsert, param);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
