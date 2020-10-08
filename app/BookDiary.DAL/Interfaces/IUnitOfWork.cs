using BookDiary.DAL.Entities;
using System;

namespace BookDiary.DAL.Interfaces
{
    interface IUnitOfWork : IDisposable
    {
        IRepository<User> Users { get; }
        void Save();
    }
}
