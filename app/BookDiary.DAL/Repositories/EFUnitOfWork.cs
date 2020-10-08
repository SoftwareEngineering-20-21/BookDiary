/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookDiary.DAL.Interfaces;
using BookDiary.DAL.Entities;
using BookDiary.DAL.EF;

namespace BookDiary.DAL.Repositories
{
    class EFUnitOfWork : IUnitOfWork
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
}*/