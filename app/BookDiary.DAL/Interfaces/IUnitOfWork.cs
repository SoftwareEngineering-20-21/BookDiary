using BookDiary.DAL.Entities;
using System;

namespace BookDiary.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> Users { get; }
        void Save();
    }
}
