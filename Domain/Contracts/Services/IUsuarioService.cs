using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Services
{
    public interface IUsuarioService
    {
        Task<Usuario> Login(Usuario usuario);
        Task<int> Logout(string token);
    }
}
