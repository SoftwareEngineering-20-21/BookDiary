using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookDiary.DAL.Interfaces;
using BookDiary.DAL.Abstractions;

namespace BookDiary.Tests.Mocks
{
    public class MockRepository<T> : IRepository<T> where T : AbstractEntity
    {
        List<T> Items;

        public MockRepository()
        {
            this.Items = new List<T>();
        }
        public IEnumerable<T> Get()
        {
            return Items;
        }

        public IEnumerable<T> GetAll()
        {
            return Items;
        }

        public T Get(int id)
        {
            return Items.Find(x => x.Id == id);
        }

        public IEnumerable<T> Get(Func<T, bool> predicate)
        {
            return Items.Where(predicate);
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return Items.Where(predicate);
        }

        public void Create(T item)
        {
            Items.Add(item);
        }

        public void Update(T item) 
        {
            var index= Items.FindIndex(x => x.Id == item.Id);
            Items[index] = item;
        }

        public void Delete(int id)
        {
            var index = Items.FindIndex(x => x.Id == id);
            Items.RemoveAt(index);
        }

        public void Save() { }

        public Task SaveAsync() { return Task.FromResult(0); }
    }
}
