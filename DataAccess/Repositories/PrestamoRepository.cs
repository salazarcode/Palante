using DataAccess.Contracts;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Microsoft.Extensions.Configuration;
using DataAccess.Repositories.Abstractions;
using System.Data.SqlClient;

namespace DataAccess.Repositories
{
    public class PrestamoRepository : SuperRepository, IPrestamoRepository
    {
        public PrestamoRepository(IConfiguration configuration) : base(configuration)
        {
        }


        List<Prestamo> IRepository<Prestamo>.All()
        {
            try
            {
                var res = Query<Prestamo>("select * from Prestamo", null);
                return res;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        int IRepository<Prestamo>.Create(Prestamo entity)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("@Capital", entity.Capital);
                parameters.Add("@ClienteID", entity.ClienteID);
                var res = Execute("insert into Prestamo values(@Capital, @ClienteID)", parameters);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        int IRepository<Prestamo>.Delete(int ID)
        {
            try
            {
                var res = Execute("delete from Prestamo where ID = @ID");
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        Prestamo IRepository<Prestamo>.Find(int ID)
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@ID", ID);
                var res = Query<Prestamo>("select * from Prestamo where ID = @ID", param);
                return res[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        int IRepository<Prestamo>.Update(Prestamo entity)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("@Capital", entity.Capital);
                parameters.Add("@ID", entity.ID);
                var res = Execute("update Cliente set capital = @Capital where ID = @ID", parameters);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
