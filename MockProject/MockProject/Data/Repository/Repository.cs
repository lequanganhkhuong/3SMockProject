using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MockProject.Data.Interface;
using MockProject.Models;

namespace MockProject.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;
        private readonly UnitOfWork _unitOfWork;

        public Repository(AppDbContext context, UnitOfWork unitOfWork)
        {
            _context = context;
            _dbSet = context.Set<T>();
            _unitOfWork = unitOfWork;
        }


        public IEnumerable<T> GetAll()
        {
            return _dbSet.AsEnumerable();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
            _unitOfWork.Commit();
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _unitOfWork.Commit();
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }
    }
}