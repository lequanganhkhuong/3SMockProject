using System.Linq;
using Microsoft.EntityFrameworkCore;
using MockProject.Data.Interface;
using MockProject.Models;

namespace MockProject.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _entity;

        public Repository(AppDbContext context)
        {
            _context = context;
            _entity = _context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            return _entity;
        }

        public T Get(int? id)
        {
            return _entity.Find(id);
        }

        public void Insert(T entity)
        {
            _entity.Add(entity);
        }

        public void Update(T entity)
        {
            _entity.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        

        public void Remove(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _entity.Attach(entity);
            }

            _entity.Remove(entity);
        }
        public void Delete(object id)
        {
            T entity = _entity.Find(id);
            Remove(entity);
        }
    }
}