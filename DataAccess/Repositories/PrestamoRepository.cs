using Microsoft.Extensions.Configuration;
using DAL.Abstractions;
using Domain.Contracts.Repositories;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain.Entities;
using System;

namespace DAL.Repositories
{
    public class PrestamoRepository : SuperRepository, IPrestamoRepository
    {
        public PrestamoRepository(IConfiguration configuration) : base(configuration)
        {
        }

        async Task<List<Prestamo>> IPrestamoRepository.ByClienteID()
        {
            try
            {
                var res = await Query<Prestamo>("select * from prestamo", null);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        async Task<int> IRepository<Prestamo>.Delete(int ID)
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@ID", ID);
                var res = await Execute("delete from prestamo where ID = @ID", param);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        async Task<List<Prestamo>> IRepository<Prestamo>.Get(int ID = 0)
        {
            try
            {
                Dictionary<string, object> param = null;
                if (ID != 0)
                {
                    param = new Dictionary<string, object>();
                    param.Add("@ID", ID);
                }

                string query = "select * from prestamo" + ( ID != 0 ? " where ID = @ID" : "" );
                
                var res = await Query<Prestamo>(query, param);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        async Task<int> IRepository<Prestamo>.Save(Prestamo entity)
        {
            Dictionary<string, object> param = new Dictionary<string, object>();

            try
            {
                if(entity.ID != 0)
                    param.Add("@ID", entity.ID);

                param.Add("@Capital", entity.Capital);
                param.Add("@ClienteID", entity.ClienteID);

                string queryInsert = "insert into Prestamo values(@Capital, @ClienteID); select @@identity;";
                string queryUpdate = "update prestamo set capital = @capital, clienteid = @ClienteID where ID = @ID; select @@identity;";

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
