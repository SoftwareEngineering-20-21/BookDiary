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
    public class StatisticRepository : IRepository<Statistic>
    {
        private AppDbContext db;
        private readonly DbSet<Statistic> dbSet;

        public StatisticRepository()
        {
            this.db = new AppDbContext();
            dbSet = db.Set<Statistic>();
            dbSet.Load();
        }

        public StatisticRepository(AppDbContext context)
        {
            this.db = context;
            dbSet = db.Set<Statistic>();
        }

        public IEnumerable<Statistic> Get()
        {
            return dbSet.ToList();
        }

        public IEnumerable<Statistic> Get(Func<Statistic, bool> predicate)
        {
            return dbSet.Where(predicate).ToList();
        }

        public IEnumerable<Statistic> GetAll()
        {
            return db.Statistics;
        }

        public Statistic Get(int id)
        {
            return dbSet.FirstOrDefault(x => x.Id == id);
        }

        public void Create(Statistic statistic)
        {
            db.Statistics.Add(statistic);
        }

        public void Update(Statistic statistic)
        {
            dbSet.Update(statistic);
        }

        public IEnumerable<Statistic> Find(Func<Statistic, bool> predicate)
        {
            return db.Statistics.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Statistic statistic = dbSet.Find(id);
            if (statistic != null)
                dbSet.Remove(statistic);
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
