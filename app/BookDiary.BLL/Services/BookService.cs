using System;
using BookDiary.BLL.DTO;
using BookDiary.DAL.Entities;
using BookDiary.DAL.Interfaces;
using BookDiary.BLL.Infrastructure;
using BookDiary.BLL.Interfaces;
using System.Collections.Generic;
using AutoMapper;

namespace BookDiary.BLL.Services
{
    public class BookService : IBookService
    {
        IUnitOfWork Database { get; set; }

        public BookService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void CreateBook(BookDTO bookDto)
        {
            Book book = new Book
            {
                Status = (DAL.Entities.BookStatus)bookDto.Status,
                Title = bookDto.Title,
                Author = bookDto.Author,
                TotalPages = bookDto.TotalPages,
                UserId = bookDto.UserId
            };

            Database.Books.Create(book);
            Database.Save();
        }

        public void UpdateBook(BookDTO bookDto)
        {
            Book book = Database.Books.Get(bookDto.Id);

            if (book == null)
            {
                throw new ValidationException("Book not found", "");
            }
            else
            {
                book.Status = (DAL.Entities.BookStatus)bookDto.Status;
                book.Title = bookDto.Title;
                book.Author = bookDto.Author;
                book.TotalPages = bookDto.TotalPages;
                book.ReadPages = bookDto.ReadPages;
                book.Review = bookDto.Review;
                book.Mark = bookDto.Mark;
                Database.Books.Update(book);
            }

            Database.Save();
        }

        public void DeleteBook(BookDTO bookDto)
        {
            Book book = Database.Books.Get(bookDto.Id);

            if (book == null)
            {
                throw new ValidationException("Book not found", "");
            }
            else
            {
                Database.Books.Delete(book.Id);
            }

            Database.Save();
        }

        public BookDTO GetBook(int? Id)
        {
            if (Id == null)
            {
                throw new ValidationException("Book id not set", "");
            }
            var book = Database.Books.Get(Id.Value);
            if (book == null)
            {
                throw new ValidationException("Book not found", "");
            }
            return new BookDTO { Status = (DTO.BookStatus)book.Status, Title = book.Title, Author = book.Author, TotalPages = book.TotalPages, ReadPages = book.ReadPages, Review = book.Review, Mark = book.Mark, UserId = book.UserId };
        }

        public IEnumerable<BookDTO> GetBooks()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Book, BookDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Book>, List<BookDTO>>(Database.Books.GetAll());
        }

        public IEnumerable<BookDTO> GetBooksByUserId(int? userId)
        {
            if (userId == null)
            {
                throw new ValidationException("User id not set", "");
            }
            User user = Database.Users.Get(userId.Value);
            if (user == null)
            {
                throw new ValidationException("User not found", "");
            }
            else
            {
                // TODO !!!!!!!!!!!!!!!!!!!!!!!!!!!!
                return null;
            }
        }
    }
}