using BookDiary.DAL.Entities;
using System;

namespace BookDiary.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<User> Users { get; }

        IRepository<Book> Books { get; }

        IRepository<Statistic> Statistics { get; }

        IRepository<Notification> Notifications { get; }

        void Save();
    }
}
