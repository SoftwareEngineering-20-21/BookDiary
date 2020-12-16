using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookDiary.DAL.Entities;
using BookDiary.DAL.Interfaces;

namespace BookDiary.Tests.Mocks
{
    public class UnitOfWork : IUnitOfWork
    {
        public IRepository<User> Users { get; }

        public IRepository<Book> Books { get; }

        public IRepository<Statistic> Statistics { get; }

        public IRepository<Notification> Notifications { get; }

        public UnitOfWork()
        {
            Users = new MockRepository<User>();
            Books = new MockRepository<Book>();
            Statistics = new MockRepository<Statistic>();
            Notifications = new MockRepository<Notification>();
        }

        public void Save() { }

        public void Dispose() { }
    }
}
