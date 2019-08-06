using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MockProject.Data.Interface;
using MockProject.Models;

namespace MockProject.Data.Repository
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly AppDbContext _context;
        private readonly UnitOfWork _unitOfWork;


        public ScheduleRepository(AppDbContext context, UnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }


        public IEnumerable<Schedule> GetAll()
        {
            return _context.Schedules;
        }

        public void Add(Schedule entity)
        {
            _context.Schedules.Add(entity);
            _unitOfWork.Commit();
        }

        public void Update(Schedule entity)
        {
            var schedule = _context.Schedules.Attach(entity);
            schedule.State = EntityState.Modified;
            _unitOfWork.Commit();
        }

        public void Delete(Schedule entity)
        {
            var schedule = _context.Schedules.Find(entity);
            if (schedule != null)
            {
                _context.Schedules.Remove(entity);
                _unitOfWork.Commit();
            }
        }

        public Schedule GetById(int id)
        {
            return _context.Schedules.Find(id);
        }
    }
}