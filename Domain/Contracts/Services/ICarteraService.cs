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
        Task<IEnumerable<Cartera>> All(int ProductoID, int EsRepro);
        Task<Cartera> Find(int CarteraID, int ProductoID);
        Task<int> Add(int CarteraID, int ProductoID, int CreditoID);
        Task<int> Remove(int CarteraID, int ProductoID, int CreditoID);
        Task<int> Cerrar(int CarteraID, int ProductoID, DateTime FechaCierre);
        Task<int> Delete(int CarteraID, int ProductoID);
    }
}
