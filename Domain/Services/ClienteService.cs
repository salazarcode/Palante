using Domain.Contracts;
using Domain.Contracts.Repositories;
using Domain.Contracts.Services;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Domain.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepo;
        private readonly ICreditoRepository _CreditoRepo;
        public ClienteService(IClienteRepository clienteRepo, ICreditoRepository CreditoRepo)
        {
            _clienteRepo = clienteRepo;
            _CreditoRepo = CreditoRepo;
        }
        
        public async Task<List<Cliente>> All()
        {
            List<Cliente> clientes = await _clienteRepo.All();

            return clientes;
        }
        
        public async Task<Cliente> Find(int ID) {
            Cliente cliente = await _clienteRepo.Find(ID);
            return cliente;
        }
        
        
    }
}
