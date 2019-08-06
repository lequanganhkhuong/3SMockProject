using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MockProject.Data.Interface;
using MockProject.Models;

namespace MockProject.Data.Repository
{
    public class TranscriptRepository : ITransciptRepository
    {
        
        private readonly AppDbContext _context;
        private readonly UnitOfWork _unitOfWork;


        public TranscriptRepository(AppDbContext context, UnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }


        public IEnumerable<Transcript> GetAll()
        {
            return _context.Transcripts;
        }

        public void Add(Transcript entity)
        {
            _context.Transcripts.Add(entity);
            _unitOfWork.Commit();
        }

        public void Update(Transcript entity)
        {
            var transcript = _context.Transcripts.Attach(entity);
            transcript.State = EntityState.Modified;
            _unitOfWork.Commit();
        }

        public void Delete(Transcript entity)
        {
            var transcript = _context.Transcripts.Find(entity);
            if (transcript != null)
            {
                _context.Transcripts.Remove(entity);
                _unitOfWork.Commit();
            }
        }

        public Transcript GetById(int id)
        {
            return _context.Transcripts.Find(id);
        }
    }
}