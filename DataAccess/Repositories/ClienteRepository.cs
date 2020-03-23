using DataAccess.Contracts;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Microsoft.Extensions.Configuration;
using DataAccess.Repositories.Abstractions;
using System.Data.SqlClient;
using System.Linq;

namespace DataAccess.Repositories
{
    public class ClienteRepository : SuperRepository, IClienteRepository
    {
        private readonly IPrestamoRepository _prestamoRepo;
        public ClienteRepository(IConfiguration configuration, IPrestamoRepository prestamoRepo) : base(configuration)
        {
            _prestamoRepo = prestamoRepo;
        }


        List<Cliente> IRepository<Cliente>.All()
        {
            try
            {
                var clientes = Query<Cliente>("select * from cliente", null);
                var prestamos = _prestamoRepo.All();

                clientes.ForEach(c =>
                {
                    c.Prestamos = prestamos.Where(x => x.ClienteID == c.ID).ToList();
                });
                return clientes;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        int IRepository<Cliente>.Create(Cliente entity)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("@Nombres", entity.Nombres);
                var res = Execute("insert into Cliente values(@Nombres)", parameters);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        int IRepository<Cliente>.Delete(int ID)
        {
            try
            {
                var res = Execute("delete from Cliente where ID = @ID");
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        Cliente IRepository<Cliente>.Find(int ID)
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@ID", ID);
                var res = Query<Cliente>("select * from cliente where ID = @ID", param);
                return res[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        List<Prestamo> IClienteRepository.Prestamos(out Cliente cliente)
        {
            throw new NotImplementedException();
        }

        int IRepository<Cliente>.Update(Cliente entity)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("@Nombres", entity.Nombres);
                parameters.Add("@ID", entity.ID);
                var res = Execute("update Cliente set nombres = @Nombres where ID = @ID", parameters);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
