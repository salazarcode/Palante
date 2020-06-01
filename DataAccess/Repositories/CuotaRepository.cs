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

        public Task<List<Cuota>> Find(Cuota c)
        {
            throw new NotImplementedException();
        }

        async Task<List<Cuota>> IRepository<Cuota>.All(Paginacion pag = null)
        {
            try
            {
                string query = @"
                    select 
                    *
                    from credcronograma cro
                    inner join (
	                    select p.nCodCred, max(p.nNroCalendario) maximo from credcronograma p group by p.nCodCred
                    ) a on a.nCodCred = cro.nCodCred and a.maximo = cro.nNroCalendario
                    ";

                var res = await Query<Cuota>(query, null);

                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        Task<int> IRepository<Cuota>.Delete(Cuota cuota)
        {
            throw new NotImplementedException();
        }


        Task<int> IRepository<Cuota>.Save(Cuota entity)
        {
            throw new NotImplementedException();
        }
    }
}
