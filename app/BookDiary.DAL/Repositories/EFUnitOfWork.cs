using BookDiary.DAL.EF;
using BookDiary.DAL.Entities;
using BookDiary.DAL.Interfaces;
using System;

namespace BookDiary.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private AppDbContext db;
        private UserRepository userRepository;

        public EFUnitOfWork(string connectionString)
        {
            db = new AppDbContext(connectionString);
        }
        public IRepository<User> Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
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