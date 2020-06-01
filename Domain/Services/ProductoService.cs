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
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _ProductoRepo;
        public ProductoService(IProductoRepository ProductoRepo)
        {
            _ProductoRepo = ProductoRepo;
        }

        async Task<List<Producto>> IProductoService.All()
        {
            try
            {
                List<Producto> res = await _ProductoRepo.All();
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    
}
