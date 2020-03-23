using DataAccess.Contracts;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Microsoft.Extensions.Configuration;
using DataAccess.Repositories.Abstractions;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class PrestamoRepository : SuperRepository, IPrestamoRepository
    {
        public PrestamoRepository(IConfiguration configuration) : base(configuration)
        {
        }


        async Task<List<Prestamo>> IRepository<Prestamo>.All()
        {
            try
            {
                var res = await Query<Prestamo>("select * from Prestamo", null);
                return res;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        async Task<int> IRepository<Prestamo>.Create(Prestamo entity)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("@Capital", entity.Capital);
                parameters.Add("@ClienteID", entity.ClienteID);
                var res = await Execute("insert into Prestamo values(@Capital, @ClienteID)", parameters);
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
                var res = await Execute("delete from Prestamo where ID = @ID");
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        async Task<Prestamo> IRepository<Prestamo>.Find(int ID)
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@ID", ID);
                var res = await Query<Prestamo>("select * from Prestamo where ID = @ID", param);
                return res[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        async Task<int> IRepository<Prestamo>.Update(Prestamo entity)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("@Capital", entity.Capital);
                parameters.Add("@ID", entity.ID);
                var res = await Execute("update Cliente set capital = @Capital where ID = @ID", parameters);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
