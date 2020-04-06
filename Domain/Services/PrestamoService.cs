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
    public class PrestamoService : IPrestamoService
    {
        private readonly IPrestamoRepository _prestamoRepo;
        public PrestamoService(IPrestamoRepository prestamoRepo)
        {
            _prestamoRepo = prestamoRepo;
        }

        async Task<List<Prestamo>> IPrestamoService.ByClienteID(int ID)
        {
            var res = await _prestamoRepo.All();
            var prestamos = res.FindAll(x => x.ClienteID == ID);
            return prestamos;
        }
    }
}
