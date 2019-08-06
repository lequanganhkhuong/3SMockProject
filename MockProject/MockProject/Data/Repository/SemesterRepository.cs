using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MockProject.Data.Interface;
using MockProject.Models;

namespace MockProject.Data.Repository
{
    public class SemesterRepository : ISemesterRepository
    {
        private readonly AppDbContext _context;
        private readonly UnitOfWork _unitOfWork;


        public SemesterRepository(AppDbContext context, UnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }


        public IEnumerable<Semester> GetAll()
        {
            return _context.Semesters;
        }

        public void Add(Semester entity)
        {
            _context.Semesters.Add(entity);
            _unitOfWork.Commit();
        }

        public void Update(Semester entity)
        {
            var semester = _context.Semesters.Attach(entity);
            semester.State = EntityState.Modified;
            _unitOfWork.Commit();
        }

        public void Delete(Semester entity)
        {
            var semester = _context.Semesters.Find(entity);
            if (semester != null)
            {
                _context.Semesters.Remove(entity);
                _unitOfWork.Commit();
            }
        }

        public Semester GetById(int id)
        {
            return _context.Semesters.Find(id);
        }
    }
}