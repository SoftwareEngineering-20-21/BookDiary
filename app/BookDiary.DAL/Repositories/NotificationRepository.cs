using BookDiary.DAL.EF;
using BookDiary.DAL.Entities;
using BookDiary.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookDiary.DAL.Repositories
{
    public class NotificationRepository : IRepository<Notification>
    {
        private AppDbContext db;
        private readonly DbSet<Notification> dbSet;

        public NotificationRepository()
        {
            this.db = new AppDbContext();
            dbSet = db.Set<Notification>();
            dbSet.Load();
        }

        public NotificationRepository(AppDbContext context)
        {
            this.db = context;
            dbSet = db.Set<Notification>();
        }

        public IEnumerable<Notification> Get()
        {
            return dbSet.ToList();
        }

        public IEnumerable<Notification> Get(Func<Notification, bool> predicate)
        {
            return dbSet.Where(predicate).ToList();
        }

        public IEnumerable<Notification> GetAll()
        {
            return db.Notifications;
        }

        public Notification Get(int id)
        {
            return dbSet.FirstOrDefault(x => x.Id == id);
        }

        public void Create(Notification notification)
        {
            db.Notifications.Add(notification);
        }

        public void Update(Notification notification)
        {
            dbSet.Update(notification);
        }

        public IEnumerable<Notification> Find(Func<Notification, bool> predicate)
        {
            return db.Notifications.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Notification notification = dbSet.Find(id);
            if (notification != null)
                dbSet.Remove(notification);
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
