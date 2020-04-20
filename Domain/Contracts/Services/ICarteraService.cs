using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Services
{
    public interface ICarteraService
    {
        Task<int> Save(Cartera cartera);
        Task<IEnumerable<Cartera>> All();
        Task<Cartera> Find(int CarteraID);
        Task<int> Add(int CarteraID, int CreditoID);
        Task<int> Remove(int CarteraID, int CreditoID);
    }
}
