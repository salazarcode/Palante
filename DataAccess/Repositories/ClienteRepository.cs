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

        public Task<List<Cliente>> Find(Cliente cliente)
        {
            throw new NotImplementedException();
        }

        async Task<List<Cliente>> IRepository<Cliente>.All(Paginacion pag = null)
        {
            throw new NotImplementedException();
        }

        async Task<int> IRepository<Cliente>.Delete(Cliente cliente)
        {
            throw new NotImplementedException();
        }

        async Task<int> IRepository<Cliente>.Save(Cliente entity)
        {
            throw new NotImplementedException();
        }
    }
}
