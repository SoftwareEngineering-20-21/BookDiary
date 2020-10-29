using BookDiary.DAL.Entities;
using System;

namespace BookDiary.DAL.Interfaces
{
    interface IUnitOfWork : IDisposable
    {
        IRepository<User> Users { get; }
        IRepository<Book> Books { get; }
        IRepository<Statistic> Statistics { get; }
        void Save();
    }
}
