using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MockProject.Data.Interface;
using MockProject.Models;

namespace MockProject.Data.Repository
{
    public class GradeRepository : IGradeRepository
    {
        private readonly AppDbContext _context;
        private readonly UnitOfWork _unitOfWork;


        public GradeRepository(AppDbContext context, UnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }


        public IEnumerable<Grade> GetAll()
        {
            return _context.Grades;
        }

        public void Add(Grade entity)
        {
            _context.Grades.Add(entity);
            _unitOfWork.Commit();
        }

        public void Update(Grade entity)
        {
            var grade = _context.Grades.Attach(entity);
            grade.State = EntityState.Modified;
            _unitOfWork.Commit();
        }

        public void Delete(Grade entity)
        {
            var faculty = _context.Grades.Find(entity);
            if (faculty != null)
            {
                _context.Grades.Remove(entity);
                _unitOfWork.Commit();
            }
        }

        public Grade GetById(int id)
        {
            return _context.Grades.Find(id);
        }
    }
}