using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Services
{
    public interface IFondeadorService
    {
        Task<List<Fondeador>> All();
        Task<int> Save(Fondeador Fondeador);
        Task<int> Delete(int FondeadorID);
        Task<Fondeador> Find(int FondeadorID);
    }
}
