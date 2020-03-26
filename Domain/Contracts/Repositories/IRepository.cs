using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts.Repositories
{
    public interface IRepository<Entity> where Entity:class
    {
        Task<List<Entity>> Get(int ID = 0);
        Task<int> Save(Entity entity);
        Task<int> Delete(int ID);
    }
}
