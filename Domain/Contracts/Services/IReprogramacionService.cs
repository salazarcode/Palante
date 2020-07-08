using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Services
{
    public interface IReprogramacionService
    {
        Task<List<Reprogramacion>> All();
        Task<int> Save(Reprogramacion Fondeador);
        Task<int> Delete(int FondeadorID);
        Task<Reprogramacion> Find(int FondeadorID);
    }
}
