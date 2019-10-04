using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace rentabike.data
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        protected DbSet<T> DbSet;
        protected DbContext dbContext;
        public BaseRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
            DbSet = dbContext.Set<T>();
        }

        public virtual T GetById(int id)
        {
            return DbSet.Find(id);
        }
        public virtual void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public virtual void Insert(T entity)
        {
            DbSet.Add(entity);
            dbContext.SaveChanges();
            
        }

        public virtual void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }

}
