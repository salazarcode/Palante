using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using DAL.Abstractions;
using System.Threading.Tasks;
using Domain.Contracts.Repositories;
using Domain.Entities;
using System.Linq;

namespace DAL.Repositories
{
    public class ClienteRepository : SuperRepository, IClienteRepository
    {
        private readonly ICreditoRepository _CreditoRepo;
        public ClienteRepository(IConfiguration configuration, ICreditoRepository CreditoRepo) : base(configuration)
        {
            _CreditoRepo = CreditoRepo;
        }

        async Task<List<Cliente>> IRepository<Cliente>.All()
        {
            try
            {
                string query = "select top 100 * from cl_cliente";

                var res = await Query<Cliente>(query, null);

                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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

        async Task<Cliente> IRepository<Cliente>.Find(int ID)
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@ID", ID);
                var res = await Query<Cliente>("select * from cliente where ID = @ID", param);
                return res.First();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        async Task<int> IRepository<Cliente>.Save(Cliente entity)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();
            /*
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
            */
            return 1;
        }
    }
}
