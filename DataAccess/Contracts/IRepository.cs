using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Contracts
{
    public interface IRepository<Entity> where Entity:class
    {
        Task<Entity> Find(int ID);
        Task<List<Entity>> All();
        Task<int> Create(Entity entity);
        Task<int> Update(Entity entity);
        Task<int> Delete(int ID);
    }
}
