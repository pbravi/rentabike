using System;
using System.Threading.Tasks;

namespace rentabike.data
{
    public interface IRepository<T> where T:class
    {
        T GetById(int id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
