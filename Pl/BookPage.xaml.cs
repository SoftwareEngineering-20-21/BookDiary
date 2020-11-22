using System;
using System.Windows;
using BookDiary.DAL.Entities;

namespace BookDiary.Pl
{
    /// <summary>
    /// Interaction logic for BookPage.xaml
    /// </summary>
    public partial class BookPage : Window
    {
        public BookPage(Book book)
        {
            InitializeComponent();

            title.Text = book.Title;
            totalPages.Text = Convert.ToString(book.TotalPages);
            author.Text = book.Author;
            /// INITIALIZATE A BOOK !!!!!!!!

        }
    }
}
