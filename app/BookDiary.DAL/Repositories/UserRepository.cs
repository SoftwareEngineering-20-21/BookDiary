using BookDiary.DAL.EF;
using BookDiary.DAL.Entities;
using BookDiary.DAL.Interfaces;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookDiary.DAL.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private AppDbContext db;
        private readonly DbSet<User> dbSet;

        public UserRepository()
        {
            this.db = new AppDbContext();
            dbSet = db.Set<User>();
            dbSet.Load();
        }

        public UserRepository(AppDbContext context)
        {
            this.db = context;
            dbSet = db.Set<User>();
        }

        public IEnumerable<User> Get()
        {
            return dbSet.ToList();
        }

        public IEnumerable<User> Get(Func<User, bool> predicate)
        {
            return dbSet.Where(predicate).ToList();
        }

        public IEnumerable<User> GetAll()
        {
            return db.Users;
        }

        public User Get(int id)
        {
            return db.Users.Find(id);
        }

        public void Create(User user)
        {
            db.Users.Add(user);
        }

        public void Update(User user)
        {
            db.Entry(user).State = EntityState.Modified;
        }

        public IEnumerable<User> Find(Func<User, bool> predicate)
        {
            return db.Users.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            User user = db.Users.Find(id);
            if (user != null)
                db.Users.Remove(user);
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