using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MockProject.Data.Interface;
using MockProject.Models;

namespace MockProject.Data.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _context;
        private readonly UnitOfWork _unitOfWork;


        public RoleRepository(AppDbContext context, UnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }


        public IEnumerable<Role> GetAll()
        {
            return _context.Roles;
        }

        public void Add(Role entity)
        {
            _context.Roles.Add(entity);
            _unitOfWork.Commit();
        }

        public void Update(Role entity)
        {
            var role = _context.Roles.Attach(entity);
            role.State = EntityState.Modified;
            _unitOfWork.Commit();
        }

        public void Delete(Role entity)
        {
            var role = _context.Roles.Find(entity);
            if (role != null)
            {
                _context.Roles.Remove(entity);
                _unitOfWork.Commit();
            }
        }

        public Role GetById(int id)
        {
            return _context.Roles.Find(id);
        }
    }
}