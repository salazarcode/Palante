using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Services
{
    public interface ICronogramaService
    {
        Task<List<Cronograma>> GetCronogramasPalante(int nCodCred);
        Task<List<Cronograma>> GetCronogramasFondeador(int nCodCred);
    }
}
