using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Repositories
{
    public interface IRepository<Entity> where Entity:class
    {
        Task<int> Save(Entity entity);
        Task<int> Delete(Entity entity);
        Task<List<Entity>> All(Paginacion paginacion = null);
        Task<List<Entity>> Find(Entity param);
    }
}
