using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Services
{
    public interface ICreditoService
    {
        Task<List<Credito>> All();
        Task<List<Credito>> ByClienteID(int ID);
        Task<List<Credito>> Cumplimiento(int FondeadorID);
    }
}
