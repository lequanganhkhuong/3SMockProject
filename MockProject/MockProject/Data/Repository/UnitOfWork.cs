using MockProject.Data.Interface;
using MockProject.Models;

namespace MockProject.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
       // private Repository<Faculty> _facutlyRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}