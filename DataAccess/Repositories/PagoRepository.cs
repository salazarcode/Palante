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
using Transversal.Util;

namespace DAL.Repositories
{
    public class PagoRepository : SuperRepository, IPagoRepository
    {
        private IMyLogger _logger;
        public PagoRepository(IConfiguration configuration, IMyLogger logger) : base(configuration)
        {
            _logger = logger;
        }

        public async Task<int> Confirmar(int PagoID)
        {
            try
            {
                string query = "exec CerrarPago @pagoID";
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@pagoID", PagoID);

                var res = await Execute(query, param);

                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<PagoDetalle>> FindDeuda(int nCodCred)
        {
            try
            {
                string query = @"exec dbo.FindDeuda @nCodCred";

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@nCodCred", nCodCred);

                var res = await Query<PagoDetalle>(query, param);

                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        async Task<List<Pago>> IRepository<Pago>.All(Paginacion paginacion)
        {
            try { 
                string query = @"exec dbo.GetPagos";

                Dictionary<string, object> param = new Dictionary<string, object>();

                var dict = new Dictionary<int, Pago>();

                using var conn = new SqlConnection(_connectionString);
                var list = await conn.QueryAsync<Pago, PagoDetalle, Fondeador, Producto, PagoConcepto, Pago>(
                    query,
                    (pago, detalle, fondeador, producto, pagoConcepto) =>
                    {
                        Pago pagoEntry;

                        if (!dict.TryGetValue(pago.PagoID, out pagoEntry))
                        {
                            pagoEntry = pago;
                            pagoEntry.Fondeador = fondeador;
                            pagoEntry.Producto = producto;
                            pagoEntry.Detalles = new List<PagoDetalle>();
                            dict.Add(pagoEntry.PagoID, pagoEntry);
                        }

                        detalle.PagoConcepto = pagoConcepto;

                        pagoEntry.Detalles.Add(detalle);
                        return pagoEntry;
                    },
                    param,
                    splitOn: "codigoFondeador,FondeadorID,nCodigo,PagoConceptoID");

                return list.Distinct().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
}

        Task<int> IPagoRepository.Crear(Pago pago)
        {
            throw new NotImplementedException();
        }

        async Task<int> IRepository<Pago>.Delete(Pago entity)
        {
            try
            {
                string query = "exec EliminarPago @pagoID";
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@pagoID", entity.PagoID);

                var res = await Execute(query, param);

                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        async Task<List<Pago>> IRepository<Pago>.Find(Pago pago)
        {
            try
            {
                string query = @"dbo.FindPago @PagoID";

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@PagoID", pago.PagoID);

                var dict = new Dictionary<int, Pago>();


                using var conn = new SqlConnection(_connectionString);
                var list = await conn.QueryAsync<Pago, PagoDetalle, Fondeador, Producto, Pago>(
                    query,
                    (pago, detalle, fondeador, producto) =>
                    {
                        Pago pagoEntry;

                        if (!dict.TryGetValue(detalle.PagoID, out pagoEntry))
                        {
                            pagoEntry = pago;
                            pagoEntry.Fondeador = fondeador;
                            pagoEntry.Producto = producto;
                            pagoEntry.Detalles = new List<PagoDetalle>();
                            dict.Add(pagoEntry.PagoID, pagoEntry);
                        }

                        pagoEntry.Detalles.Add(detalle);
                        return pagoEntry;
                    },
                    param,
                    splitOn: "PagoID,FondeadorID,nCodigo");

                return list.Distinct().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        async Task<int> IRepository<Pago>.Save(Pago pago)
        {
            try
            {
                List<string> ensamblado = pago.Detalles.Select(x =>
                {
                    List<string> campos = new List<string>();
                    campos.Add(x.codigoFondeador.ToString());
                    campos.Add(x.nNroCuota.ToString());
                    campos.Add(x.Monto.ToString());
                    campos.Add(x.EsDeuda?"1":"0");

                    return String.Join(',', campos);
                }).ToList();

                var pagos = String.Join(";", ensamblado);

                await _logger.Log("Nuevo pago: 1.Fondeador: " + pago.Fondeador.FondeadorID + " Producto: " + pago.Producto.nValor + " EsMochila: " + pago.EsMochila + " Creador: " + pago.CreadoPor + " Total del pago: " + pago.Detalles.Sum(x => x.Monto) + " Pagos: " + pagos);
                
                string query = "dbo.CrearPago @pago, @f, @p, @EsMochila, @creador, @pagos";

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@pago", pago.PagoID);
                param.Add("@f", pago.Fondeador.FondeadorID);
                param.Add("@p", pago.Producto.nValor);
                param.Add("@EsMochila", pago.EsMochila);
                param.Add("@creador", pago.CreadoPor);
                param.Add("@pagos", pagos);

                var res = await Execute(query, param);

                return res;
            }
            catch (Exception ex)
            {
                await _logger.Log("Error en Nuevo pago: " + ex.Message + ", Clase: " + this.GetType().FullName);
                throw ex;
            }
        }
    }
}
