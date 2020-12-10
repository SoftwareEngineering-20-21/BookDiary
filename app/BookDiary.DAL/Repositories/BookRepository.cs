using BookDiary.DAL.EF;
using BookDiary.DAL.Entities;
using BookDiary.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookDiary.DAL.Repositories
{
    public class BookRepository : IRepository<Book>
    {
        private AppDbContext db;
        private readonly DbSet<Book> dbSet;

        public BookRepository(AppDbContext context)
        {
            this.db = context;
        }

        public IEnumerable<Book> Get()
        {
            return dbSet.ToList();
        }

        public IEnumerable<Book> Get(Func<Book, bool> predicate)
        {
            return db.Books.Where(predicate).ToList();
        }

        public IEnumerable<Book> GetAll()
        {
            return db.Books;
        }

        public Book Get(int id)
        {
            return db.Books.Find(id);
        }

        public void Create(Book book)
        {
            db.Books.Add(book);
        }

        public void Update(Book book)
        {
            db.Entry(book).State = EntityState.Modified;
        }

        public IEnumerable<Book> Find(Func<Book, bool> predicate)
        {
            return db.Books.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Book book = db.Books.Find(id);
            if (book != null)
                db.Books.Remove(book);
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
