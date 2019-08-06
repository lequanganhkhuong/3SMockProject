using MockProject.Models;

namespace MockProject.Data.Interface
{
    public interface IUnitOfWork
    {
        IRepository<User> UserRepository { get; }
        IRepository<Faculty> FacultyRepository { get; }
        IRepository<Subject> SubjectRepository { get; }
        IRepository<Role> RoleRepository { get; }
        void Save();
    }
}