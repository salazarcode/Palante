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

namespace DAL.Repositories
{
    public class CreditoRepository : SuperRepository, ICreditoRepository
    {
        public CreditoRepository(IConfiguration configuration) : base(configuration)
        {
        }

        async Task<List<Credito>> IRepository<Credito>.All()
        {
            try
            {
                string query = "select * from creditos where nEstado = 1 ";
                var res = await Query<Credito>(query, null);

                return res.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        async Task<List<Credito>> ICreditoRepository.ByClienteID(int ClienteID)
        {
            try
            {
                string query = @"
                                select * 
                                from creditos c
                                inner join CredPersonas cp on cp.nCodCred = c.nCodCred
                                where 
	                                cp.nCodPers = @ClienteID
	                                and cp.nRelacion = 10";

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@ClienteID", ClienteID);

                var res = await Query<Credito>(query, param);
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        Task<int> IRepository<Credito>.Delete(int ID)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Credito>> Cumplimiento(int FondeadorID)
        {
            try
            {
                string query = "exec VentaCartera.EvaluaCreditos " + FondeadorID;
                var res = await Query<Credito>(query, null);

                return res.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        async Task<Credito> IRepository<Credito>.Find(int ID)
        {
            try
            {
                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@ID", ID);
                var res = await Query<Credito>("select * from Credito where nCodCred = @ID", param);
                return res.First();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        Task<int> IRepository<Credito>.Save(Credito entity)
        {
            throw new NotImplementedException();
        }
    }
}
