using Domain.Contracts;
using Domain.Contracts.Repositories;
using Domain.Contracts.Services;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace Domain.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _UsuarioRepo;
        public UsuarioService(IUsuarioRepository UsuarioRepo)
        {
            _UsuarioRepo = UsuarioRepo;
        }

        async Task<Usuario> IUsuarioService.Login(Usuario usuario)
        {
            try
            {
                Usuario res = await _UsuarioRepo.Login(usuario);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        async Task<int> IUsuarioService.Logout(string token)
        {
            try
            {
                var res = await _UsuarioRepo.Logout(token);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    
}
