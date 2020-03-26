using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Services
{
    public interface IPrestamoService
    {
        Task<List<Prestamo>> ByClienteID(List<int> ids);
    }
}
