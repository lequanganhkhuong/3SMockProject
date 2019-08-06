using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MockProject.Data.Interface;
using MockProject.Models;

namespace MockProject.Data.Repository
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly AppDbContext _context;
        private readonly UnitOfWork _unitOfWork;


        public SubjectRepository(AppDbContext context, UnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }


        public IEnumerable<Subject> GetAll()
        {
            return _context.Subjectses;
        }

        public void Add(Subject entity)
        {
            _context.Subjectses.Add(entity);
            _unitOfWork.Commit();
        }

        public void Update(Subject entity)
        {
            var subject = _context.Subjectses.Attach(entity);
            subject.State = EntityState.Modified;
            _unitOfWork.Commit();
        }

        public void Delete(Subject entity)
        {
            var subject = _context.Subjectses.Find(entity);
            if (subject != null)
            {
                _context.Subjectses.Remove(entity);
                _unitOfWork.Commit();
            }
        }

        public Subject GetById(int id)
        {
            return _context.Subjectses.Find(id);
        }
    }
}