using Domain.Contracts;
using Domain.Contracts.Repositories;
using Domain.Contracts.Services;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using Domain.ValueObjects;

namespace Domain.Services
{
    public class ReporteService : IReporteService
    {
        private readonly IReporteRepository _ReporteRepo;
        public ReporteService(IReporteRepository ReporteRepo)
        {
            _ReporteRepo = ReporteRepo;
        }

        async Task<List<ClasificacionesCSV>> IReporteService.GetClasificacionesCSV(int carteraid, int ProductoID)
        {
            try
            {
                var res = await _ReporteRepo.GetClasificacionesCSV(carteraid,  ProductoID);
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        async Task<List<ResumenYapamotors>> IReporteService.ResumenYapamotors(int carteraid, int ProductoID)
        {
            try
            {
                var res = await _ReporteRepo.ResumenYapamotors(carteraid,  ProductoID);
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        async Task<List<AnexoYapamotors>> IReporteService.AnexoYapamotors(int carteraid, int ProductoID)
        {
            try
            {
                var res = await _ReporteRepo.AnexoYapamotors(carteraid,  ProductoID);
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        async Task<List<ClientesCSV>> IReporteService.GetClientesCSV(int carteraid, int ProductoID)
        {
            try
            {
                var res = await _ReporteRepo.GetClientesCSV(carteraid, ProductoID);
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        async Task<List<CreditosCSV>> IReporteService.GetCreditosCSV(int carteraid, int ProductoID)
        {
            try
            {
                var res = await _ReporteRepo.GetCreditosCSV(carteraid,  ProductoID);
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        async Task<List<CronogramasCSV>> IReporteService.GetCronogramasCSV(int carteraid, int ProductoID)
        {
            try
            {
                var res = await _ReporteRepo.GetCronogramasCSV(carteraid,  ProductoID);
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
