using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Services
{
    public interface IClienteService
    {
        Task<List<Cliente>> All();
        Task<Cliente> Find(int ID);
    }
}
