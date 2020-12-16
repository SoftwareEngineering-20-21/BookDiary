using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using BookDiary.BLL.DTO;
using BookDiary.BLL.Interfaces;
using BookDiary.DAL.Entities;
using Ninject;

namespace BookDiary.PL
{
    /// <summary>
    /// Interaction logic for BookPage.xaml
    /// </summary>
    public partial class BookPage : Window
    {
        private BookDTO book;

        private StatisticDTO statistic;

        private int pages;

        private IKernel container;

        private IBookService bookService;

        private IStatisticService statisticService;
        public BookPage(IKernel container, BookDTO book)
        {
            this.container = container;
            this.bookService = container.Get<IBookService>();
            statisticService = container.Get<IStatisticService>();
            List<StatisticDTO> statistics = statisticService.GetStatistics().Where(x => x.BookId == book.Id && x.Day.Date == DateTimeOffset.Now.Date).ToList();
            if (statistics.Count == 0)
            {
                statisticService.CreateStatistic(new StatisticDTO
                {
                    BookId = book.Id,
                    Day = DateTimeOffset.Now.Date,
                    OldPages = 0,
                    NewPages = 0
            });
            }
            statistics = statisticService.GetStatistics().Where(x => x.BookId == book.Id && x.Day.Date == DateTimeOffset.Now.Date).ToList();
            statistic = statistics.ElementAt(0);
            
            InitializeComponent();
            
            title.Text = book.Title;
            totalPages.Text = Convert.ToString(book.TotalPages);
            author.Text = book.Author;
            readPages.Text = book.ReadPages.ToString();
            todayReadPages.Text = statistic.NewPages.ToString();
            Mark.Text = book.Mark.ToString();
            Review.Text = book.Review;
            Save.IsEnabled = false;
            this.book = book;
        }

        private void DeleteBook_Click(object sender, RoutedEventArgs e)
        {
            bookService.DeleteBook(book);
            this.Close();
        }
        private void EditBook_Click(object sender, RoutedEventArgs e)
        {
            totalPages.IsReadOnly = false;
            author.IsReadOnly = false;
            todayReadPages.IsReadOnly = false;
            if (book.Status == BookStatus.Completed) Mark.IsReadOnly = false;
            if (book.Status == BookStatus.Completed) Review.IsReadOnly = false;

            totalPages.BorderThickness = new Thickness(2);
            author.BorderThickness = new Thickness(2);
            todayReadPages.BorderThickness = new Thickness(2);
            if (book.Status == BookStatus.Completed) Mark.BorderThickness = new Thickness(2);
            if (book.Status == BookStatus.Completed) Review.BorderThickness = new Thickness(2);

            pages = Convert.ToInt32(todayReadPages.Text);
            todayReadPages.Text = "0";
            Save.IsEnabled = true;

        }
        private void SaveBook_Click(object sender, RoutedEventArgs e)
        {
            Save.IsEnabled = false;
            totalPages.IsReadOnly = true;
            author.IsReadOnly = true;
            todayReadPages.IsReadOnly = true;
            Mark.IsReadOnly = true;
            Review.IsReadOnly = true;

            totalPages.BorderThickness = new Thickness(0);
            author.BorderThickness = new Thickness(0);
            todayReadPages.BorderThickness = new Thickness(0);
            Mark.BorderThickness = new Thickness(0);
            Review.BorderThickness = new Thickness(0);

            int ttlpages = Convert.ToInt32(totalPages.Text);
            int readpages = Convert.ToInt32(readPages.Text);
            
            readpages += Convert.ToInt32(todayReadPages.Text);
            todayReadPages.Text = Convert.ToString(Convert.ToInt32(todayReadPages.Text) + statistic.NewPages);
            statistic.NewPages = Convert.ToInt32(todayReadPages.Text);
            statistic.Day = DateTimeOffset.Now.Date;
            statisticService.UpdateStatistic(statistic);
            
            if (readpages > 0 && readpages < ttlpages)
            {
                book.Status = BookStatus.InProgress;
            }
            else if (readpages >= ttlpages)
            {
                book.Status = BookStatus.Completed;
                readpages = ttlpages;
            }
            book.Title = title.Text;
            book.Author = author.Text;
            book.TotalPages = ttlpages;
            book.ReadPages = readpages;
            book.Mark = Convert.ToInt32(Mark.Text);
            book.Review = Review.Text;
            
            readPages.Text = Convert.ToString(readpages);
            
            bookService.UpdateBook(book);
        }
        private void NotificationBook_Click(object sender, RoutedEventArgs e)
        {
            BookNotificationPage bn = new BookNotificationPage(container, book);
            bn.Show();
        }

        private void StatisticBook_Click(object sender, RoutedEventArgs e)
        {
            BookStatisticPage bs = new BookStatisticPage(container, book);
            bs.Show();
        }
    }
}
