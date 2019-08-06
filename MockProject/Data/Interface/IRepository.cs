using System.Linq;

namespace MockProject.Data.Interface
{
    public interface IRepository<T> where T: class
    {
        IQueryable < T > GetAll();  
        T Get(int? id);  
        void Insert(T entity);  
        void Update(T entity);  
        void Remove(T entity);
    }
}