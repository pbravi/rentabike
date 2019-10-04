using rentabike.data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace rentabike.service
{
    public abstract class BaseService<T> where T:class
    {
        private readonly IRepository<T> repository;

        public BaseService(IRepository<T> repository)
        {
            this.repository = repository;
        }
        public T GetById(int id)
        {
            return repository.GetById(id);
        }
        public void Insert(T entity)
        {
            repository.Insert(entity);
        }

        public void Update(T entity)
        {
            repository.Update(entity);
        }
        public void Delete(int id)
        {
            repository.Delete(id);
        }
    }
}
