using Domain.Entities;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Repositories
{
    public interface IReporteRepository
    {
        Task<List<ClientesCSV>> GetClientesCSV(int CarteraID, int ProductoID);
        Task<List<CronogramasCSV>> GetCronogramasCSV(int CarteraID, int ProductoID);
        Task<List<CreditosCSV>> GetCreditosCSV(int CarteraID, int ProductoID);
        Task<List<ClasificacionesCSV>> GetClasificacionesCSV(int CarteraID, int ProductoID);
        Task<List<ResumenYapamotors>> ResumenYapamotors(int CarteraID, int ProductoID);
        Task<List<AnexoYapamotors>> AnexoYapamotors(int CarteraID, int ProductoID);
    }
}
