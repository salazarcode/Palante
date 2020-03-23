using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Contracts
{
    public interface IRepository<Entity> where Entity:class
    {
        Entity Find(int ID);
        List<Entity> All();
        int Create(Entity entity);
        int Update(Entity entity);
        int Delete(int ID);
    }
}
