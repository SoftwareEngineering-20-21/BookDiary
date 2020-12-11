using BookDiary.DAL.EF;
using BookDiary.DAL.Entities;
using BookDiary.DAL.Interfaces;
using System;

namespace BookDiary.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private AppDbContext db;
        public IRepository<User> Users { get; }
        public IRepository<Book> Books { get; }
        public IRepository<Notification> Notifications { get; }
        public IRepository<Statistic> Statistics { get; }

        public EFUnitOfWork(
            AppDbContext dbContext,
            IRepository<User> userRepository,
            IRepository<Book> bookRepository,
            IRepository<Notification> notificationRepository,
            IRepository<Statistic> statisticRepository)
        {
            this.db = dbContext;
            Users = userRepository;
            Books = bookRepository;
            Notifications = notificationRepository;
            Statistics = statisticRepository;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}