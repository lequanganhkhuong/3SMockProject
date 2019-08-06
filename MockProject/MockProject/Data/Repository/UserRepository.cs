using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MockProject.Data.Interface;
using MockProject.Models;

namespace MockProject.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly UnitOfWork _unitOfWork;


        public UserRepository(AppDbContext context, UnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }


        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public void Add(User entity)
        {
            _context.Users.Add(entity);
            _unitOfWork.Commit();
        }

        public void Update(User entity)
        {
            var user = _context.Users.Attach(entity);
            user.State = EntityState.Modified;
            _unitOfWork.Commit();
        }

        public void Delete(User entity)
        {
            var user = _context.Users.Find(entity);
            if (user != null)
            {
                _context.Users.Remove(entity);
                _unitOfWork.Commit();
            }
        }

        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }
    }
}