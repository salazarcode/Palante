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

        Task<List<Prestamo>> IPrestamoService.ByClienteID()
        {
            try
            {
                var res = _prestamoRepo.ByClienteID();
                return res;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
