using System.Collections.Generic;
using System.Threading.Tasks;

namespace MockProject.Data.Interface
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        T GetById(int id);
    }
}