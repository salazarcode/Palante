using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Repositories
{
    public interface IRepository<Entity> where Entity:class
    {
        Task<int> Save(Entity entity);
        Task<int> Delete(int ID);
        Task<List<Entity>> All();
        Task<Entity> Find(int ID);
    }
}
