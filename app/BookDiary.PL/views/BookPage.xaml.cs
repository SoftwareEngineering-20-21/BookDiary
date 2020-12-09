using System;
using System.Windows;
using BookDiary.DAL.Entities;

namespace BookDiary.PL
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

        private void DeleteBook_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void StatisticBook_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
