﻿ using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookDiary.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> Get();
        IEnumerable<T> GetAll();
        T Get(int id);
        IEnumerable<T> Find(Func<T, bool> predicate);
        IEnumerable<T> Get(Func<T, bool> predicate);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        void Save();
        Task SaveAsync();
    }
}
