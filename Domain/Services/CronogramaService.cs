using Domain.Contracts;
using Domain.Contracts.Repositories;
using Domain.Contracts.Services;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace Domain.Services
{
    public class CronogramaService : ICronogramaService
    {
        private readonly ICronogramaRepository _CronogramaRepo;
        public CronogramaService(ICronogramaRepository CronogramaRepo)
        {
            _CronogramaRepo = CronogramaRepo;
        }

        public async Task<List<Cronograma>> GetCronogramasPalante(string codigo, bool ConUltimoCalendario = false)
        {
            try
            {
                var res = await _CronogramaRepo.GetCronogramasPalante(codigo, ConUltimoCalendario);
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<Cronograma>> GetCronogramasFondeador(string codigo, bool ConUltimoCalendario = false)
        {
            try
            {
                var res = await _CronogramaRepo.GetCronogramasFondeador(codigo, ConUltimoCalendario);
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
