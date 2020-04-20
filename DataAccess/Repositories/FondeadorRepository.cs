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
    public class FondeadorRepository : SuperRepository, IFondeadorRepository
    {
        public FondeadorRepository(IConfiguration configuration) : base(configuration)
        {
        }
        async Task<List<Fondeador>> IRepository<Fondeador>.All()
        {
            try
            {
                string query = "select * from ventacartera.fondeadores";
                var res = await Query<Fondeador>(query, null);

                return res.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        async Task<int> IRepository<Fondeador>.Delete(int ID)
        {
            try
            {
                string query = "delete from VentaCartera.Fondeadores where FondeadorID = @ID";

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@ID", ID);

                var res = await Execute(query, param);

                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        async Task<Fondeador> IRepository<Fondeador>.Find(int FondeadorID)
        {
            try
            {
                string query = "select * from ventacartera.fondeadores where FondeadorID = @FondeadorID";

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@FondeadorID", FondeadorID);

                var res = await Query<Fondeador>(query, param);

                return res.ToList().First();
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
                        string query = "insert into VentaCartera.Fondeadores values(@Nombre, @Color, @Evaluador)";

                        Dictionary<string, object> param = new Dictionary<string, object>();
                        param.Add("@Nombre", entity.Nombre);
                        param.Add("@Color", entity.Color);
                        param.Add("@Evaluador", entity.Evaluador);

                        var res = await Execute(query, param);

                        return res;
                }
                else
                {
                    string query = "update VentaCartera.Fondeadores set Nombre = @Nombre, Color = @Color, Evaluador = @Evaluador where FondeadorID = @FondeadorID";

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
