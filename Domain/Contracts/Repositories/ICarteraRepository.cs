using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Repositories
{
    public interface ICarteraRepository : IRepository<Cartera>
    {
        Task<int> Add(int CarteraID, int CreditoID);
        Task<int> Remove(int CarteraID, int CreditoID);
    }
}
