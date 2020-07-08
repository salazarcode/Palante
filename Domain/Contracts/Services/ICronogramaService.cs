using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Services
{
    public interface ICronogramaService
    {
        Task<List<Cronograma>> GetCronogramasPalante(string codigo, bool ConUltimoCalendario);
        Task<List<Cronograma>> GetCronogramasFondeador(string codigo, bool ConUltimoCalendario);
    }
}
