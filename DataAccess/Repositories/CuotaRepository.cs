using Microsoft.Extensions.Configuration;
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

namespace DAL.Repositories
{
    public class CuotaRepository : SuperRepository, ICuotaRepository
    {
        public CuotaRepository(IConfiguration configuration) : base(configuration)
        {
        }
        public Task<List<Cuota>> All(Paginacion paginacion = null)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(Cuota entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<Cuota>> Find(Cuota param)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Cuota>> GetCuotas(DateTime pagosDesde, DateTime pagosHasta, string nEstadoCuota)
        {
            try
            {
                string q = "exec dbo.GetCuotas @pagosDesde, @pagosHasta, @nEstadoCuota";

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@pagosDesde", pagosDesde);
                param.Add("@pagosHasta", pagosHasta);
                param.Add("@nEstadoCuota", nEstadoCuota);

                var list = await Query<Cuota>(q, param);

                list.ForEach(ele => {
                    var n = ele.producto;
                    if (n == 9 || n == 8 || n == 7 || n == 6 || n == 2)
                    {
                        ele.producto = 2;
                    }
                });

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Cuota>> GetCuotasFondeador(string buscar, DateTime cuotasHasta, string estados)
        {
            try
            {
                string q = "exec dbo.GetCuotasFondeador @buscar, @cuotasHasta, @estados";

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@buscar", buscar == null ? "" : buscar);
                param.Add("@cuotasHasta", cuotasHasta);
                param.Add("@estados", estados);

                return await Query<Cuota>(q, param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Cuota>> GetCuotasPorVencer(DateTime pagosDesde, DateTime pagosHasta, string codigoFondeador)
        {
            try
            {
                string q = "exec dbo.GetCuotasPorVencer @pagosDesde, @pagosHasta, @codigoFondeador";

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@pagosDesde", pagosDesde);
                param.Add("@pagosHasta", pagosHasta);
                param.Add("@codigoFondeador", codigoFondeador);

                var list = await Query<Cuota>(q, param);

                list.ForEach(ele => {
                    var n = ele.producto;
                    if (n == 9 || n == 8 || n == 7 || n == 6 || n == 2)
                    {
                        ele.producto = 2;
                    }
                });

                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<int> Save(Cuota entity)
        {
            throw new NotImplementedException();
        }
    }
}
