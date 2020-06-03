﻿using Microsoft.Extensions.Configuration;
using DAL.Abstractions;
using Domain.Contracts.Repositories;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain.Entities;
using System;
using System.Linq;
using Dapper;
using Dapper.Mapper;
using System.Data.SqlClient;
using Domain.ValueObjects;

namespace DAL.Repositories
{
    public class ReporteRepository : SuperRepository, IReporteRepository
    {
        public ReporteRepository(IConfiguration configuration) : base(configuration)
        {

        }

        async Task<List<ClasificacionesCSV>> IReporteRepository.GetClasificacionesCSV(int carteraid, int ProductoID)
        {

            try
            {
                string query = "exec dbo.CalificacionesCSV @carteraid, @ProductoID";
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@carteraid", carteraid);
                param.Add("@ProductoID", ProductoID);
                var res = await Query<ClasificacionesCSV>(query, param);

                return res.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        async Task<List<ResumenYapamotors>> IReporteRepository.ResumenYapamotors(int carteraid, int ProductoID)
        {

            try
            {
                string query = "exec dbo.ResumenYapamotors @carteraid, @ProductoID";
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@carteraid", carteraid);
                param.Add("@ProductoID", ProductoID);
                var res = await Query<ResumenYapamotors>(query, param);

                return res.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        async Task<List<AnexoYapamotors>> IReporteRepository.AnexoYapamotors(int carteraid, int ProductoID)
        {

            try
            {
                string query = "exec dbo.AnexoYapamotors @carteraid, @ProductoID";
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@carteraid", carteraid);
                param.Add("@ProductoID", ProductoID);
                var res = await Query<AnexoYapamotors>(query, param);

                return res.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        async Task<List<ClientesCSV>> IReporteRepository.GetClientesCSV(int carteraid, int ProductoID)
        {
            try
            {
                string query = "exec dbo.clientescsv @carteraid, @ProductoID";
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@carteraid", carteraid);
                param.Add("@ProductoID", ProductoID);
                var res = await Query<ClientesCSV>(query, param);

                return res.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        async Task<List<CreditosCSV>> IReporteRepository.GetCreditosCSV(int carteraid, int ProductoID)
        {
            try
            {
                string query = "exec dbo.creditoscsv @carteraid, @ProductoID";
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@carteraid", carteraid);
                param.Add("@ProductoID", ProductoID);
                var res = await Query<CreditosCSV>(query, param);

                return res.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        async Task<List<CronogramasCSV>> IReporteRepository.GetCronogramasCSV(int carteraid, int ProductoID)
        {
            try
            {
                string query = "exec dbo.cronogramascsv @carteraid, @ProductoID";
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@carteraid", carteraid);
                param.Add("@ProductoID", ProductoID);
                var res = await Query<CronogramasCSV>(query, param);

                return res.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}