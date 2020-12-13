using System;
using System.Windows;
using BookDiary.BLL.DTO;
using BookDiary.BLL.Interfaces;
using Ninject;

namespace BookDiary.PL
{
    /// <summary>
    /// Interaction logic for BookPage.xaml
    /// </summary>
    public partial class BookPage : Window
    {
        private BookDTO book;

        private IKernel container;

        private IBookService bookService;
        public BookPage(IKernel container, BookDTO book)
        {
            this.container = container;
            this.bookService = container.Get<IBookService>();
            InitializeComponent();

            title.Text = book.Title;
            totalPages.Text = Convert.ToString(book.TotalPages);
            author.Text = book.Author;

            this.book = book;
        }

        private void DeleteBook_Click(object sender, RoutedEventArgs e)
        {
            bookService.DeleteBook(book);
            this.Hide();
        }

        private void NotificationBook_Click(object sender, RoutedEventArgs e)
        {
            BookNotificationPage bn = new BookNotificationPage(container, book);
            bn.Show();
        }

        private void StatisticBook_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
