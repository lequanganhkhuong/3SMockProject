using System;

namespace MockProject.Data.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}