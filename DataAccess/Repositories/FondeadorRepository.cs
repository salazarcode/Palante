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
using Transversal.Util;

namespace DAL.Repositories
{
    public class FondeadorRepository : SuperRepository, IFondeadorRepository
    {
        public FondeadorRepository(IConfiguration configuration) : base(configuration)
        {
        }
        async Task<List<Fondeador>> IRepository<Fondeador>.All(Paginacion pag = null)
        { 
            try
            {

                string query = "select * from fondeadores";
                var res = await Query<Fondeador>(query, null);

                return res.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        async Task<int> IRepository<Fondeador>.Delete(Fondeador f)
        {
            try
            {
                string query = "delete from Fondeadores where FondeadorID = @ID";

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@ID", f.FondeadorID);

                var res = await Execute(query, param);

                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        async Task<List<Fondeador>> IRepository<Fondeador>.Find(Fondeador f)
        {
            try
            {
                string query = "select * from fondeadores where FondeadorID = convert(int,@FondeadorID)";

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@FondeadorID", f.FondeadorID);

                var res = await Query<Fondeador>(query, param);

                return res.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        async Task<int> IRepository<Fondeador>.Save(Fondeador entity)
        {
            try
            {
                if (entity.FondeadorID == 0)
                {
                        string query = "insert into Fondeadores values(@Nombre, @Color, @Evaluador)";

                        Dictionary<string, object> param = new Dictionary<string, object>();
                        param.Add("@Nombre", entity.Nombre);
                        param.Add("@Color", entity.Color);
                        param.Add("@Evaluador", entity.Evaluador);

                        var res = await Execute(query, param);

                        return res;
                }
                else
                {
                    string query = "update Fondeadores set Nombre = @Nombre, Color = @Color, Evaluador = @Evaluador where FondeadorID = @FondeadorID";

                    Dictionary<string, object> param = new Dictionary<string, object>();
                    param.Add("@FondeadorID", entity.FondeadorID);
                    param.Add("@Nombre", entity.Nombre);
                    param.Add("@Color", entity.Color);
                    param.Add("@Evaluador", entity.Evaluador);

                    var res = await Execute(query, param);

                    return res;
                }                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
