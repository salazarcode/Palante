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
    public class UsuarioRepository : SuperRepository, IUsuarioRepository
    {
        public UsuarioRepository(IConfiguration configuration) : base(configuration)
        {

        }

        Task<List<Usuario>> IRepository<Usuario>.All(Paginacion pag = null)
        {
            throw new NotImplementedException();
        }

        async Task<Usuario> IUsuarioRepository.Login(Usuario usuario)
        {
            try
            {
                string query = "exec LoginSP @usuario, @clave";

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@usuario", usuario.Nombre);
                param.Add("@clave", usuario.Clave);

                using var conn = new SqlConnection(_connectionString);
                var res = await conn.QueryAsync<Usuario, Rol, Usuario>(
                    query,
                    (u, r) =>
                    {
                        u.Rol = r;
                        return u;
                    },
                    param,
                    splitOn: "RolID"
                );

                return res.ToList().Count == 0 ? null : res.ToList().First();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        async Task<int> IUsuarioRepository.Logout(string token)
        {
            try
            {
                string query = "exec CerrarSesionSP @token";

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@token", token);

                var res = await Execute(query, param);

                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        Task<int> IRepository<Usuario>.Delete(Usuario ID)
        {
            throw new NotImplementedException();
        }

        Task<int> IRepository<Usuario>.Save(Usuario entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<Usuario>> Find(Usuario query)
        {
            throw new NotImplementedException();
        }
    }
}
