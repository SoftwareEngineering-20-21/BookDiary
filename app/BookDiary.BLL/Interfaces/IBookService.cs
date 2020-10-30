using System;
using System.Collections.Generic;
using BookDiary.BLL.DTO;

namespace BookDiary.BLL.Interfaces
{
    public interface IBookService
    {
        void CreateBook(BookDTO bookDTO);
        void UpdateBook(BookDTO bookDTO);
        void DeleteBook(BookDTO bookDTO);
        BookDTO GetBook(int? id);
        IEnumerable<BookDTO> GetBooks();
        IEnumerable<BookDTO> GetBooksByUserId(int? userId);
    }
}