using BookDiary.DAL.EF;
using BookDiary.DAL.Entities;
using BookDiary.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace BookDiary.DAL.Repositories
{
    public class NotificationRepository : IRepository<Notification>
    {
        private AppDbContext db;
        private readonly DbSet<Notification> dbSet;

        public NotificationRepository(AppDbContext context)
        {
            this.db = context;
        }

        public IEnumerable<Notification> Get()
        {
            return dbSet.ToList();
        }

        public IEnumerable<Notification> Get(Func<Notification, bool> predicate)
        {
            return db.Notifications.Where(predicate).ToList();
        }

        public IEnumerable<Notification> GetAll()
        {
            return db.Notifications;
        }

        public Notification Get(int id)
        {
            return db.Notifications.Find(id);
        }

        public void Create(Notification notification)
        {
            db.Notifications.Add(notification);
        }

        public void Update(Notification notification)
        {
            db.Entry(notification).State = EntityState.Modified;
        }

        public IEnumerable<Notification> Find(Func<Notification, bool> predicate)
        {
            return db.Notifications.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Notification notification = db.Notifications.Find(id);
            if (notification != null)
                db.Notifications.Remove(notification);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}
