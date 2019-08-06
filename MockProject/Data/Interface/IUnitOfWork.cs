using MockProject.Models;

namespace MockProject.Data.Interface
{
    public interface IUnitOfWork
    {
        IRepository<User> UserRepository { get; }
        void Save();
    }
}