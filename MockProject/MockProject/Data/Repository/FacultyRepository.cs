using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MockProject.Data.Interface;
using MockProject.Models;

namespace MockProject.Data.Repository
{
    public class FacultyRepository : IFacultyRepository
    {
        private readonly AppDbContext _context;
        private readonly UnitOfWork _unitOfWork;


        public FacultyRepository(AppDbContext context, UnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }


        public IEnumerable<Faculty> GetAll()
        {
            return _context.Faculties;
        }

        public void Add(Faculty entity)
        {
            _context.Faculties.Add(entity);
            _unitOfWork.Commit();
        }

        public void Update(Faculty entity)
        {
            var facutly = _context.Faculties.Attach(entity);
            facutly.State = EntityState.Modified;
            _unitOfWork.Commit();
        }

        public void Delete(Faculty entity)
        {
            var faculty = _context.Faculties.Find(entity);
            if (faculty != null)
            {
                _context.Faculties.Remove(entity);
                _unitOfWork.Commit();
            }
        }

        public Faculty GetById(int id)
        {
            return _context.Faculties.Find(id);
        }
    }
}