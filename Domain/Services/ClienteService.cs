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
        private readonly IPrestamoRepository _prestamoRepo;
        public ClienteService(IClienteRepository clienteRepo, IPrestamoRepository prestamoRepo)
        {
            _clienteRepo = clienteRepo;
            _prestamoRepo = prestamoRepo;
        }
        
        public async Task<List<Cliente>> Get(int ID = 0)
        {
            List<Cliente> clientes = await _clienteRepo.Get(ID);
            List<Prestamo> prestamos = await _prestamoRepo.ByClienteID(clientes.Select(x=>x.ID).ToList());

            clientes.ForEach(x => {
                x.Prestamos = prestamos.Where(z => z.ClienteID == x.ID).ToList();
            });

            return clientes;
        } 
        
    }
}
