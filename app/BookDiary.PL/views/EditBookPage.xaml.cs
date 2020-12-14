using Ninject;
using System;
using System.Windows;
using BookDiary.BLL.Interfaces;
using BookDiary.BLL.DTO;
using BookDiary.DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace BookDiary.PL
{
    /// <summary>
    /// Interaction logic for EditBookPage.xaml
    /// </summary>
    public partial class EditBookPage : Window
    {
        BookPage bookPage;

        private IKernel container;

        private BookDTO book;

        private List<StatisticDTO> statistic;

        private IBookService bookService;

        private IStatisticService statisticService;

        public EditBookPage(IKernel container, BookDTO book)
        {
            InitializeComponent();
            bookService = container.Get<IBookService>();
            statisticService = container.Get<IStatisticService>();
            statisticService.CreateStatistic(
                new StatisticDTO
                {
                    Day = DateTimeOffset.Now,
                    NewPages = 0,
                    OldPages = 0,
                    BookId = book.Id
                    
                });
            IEnumerable<StatisticDTO> statistics = statisticService.GetStatisticsByBookId(book.Id);
            statistic = statistics.Where(x => x.BookId == book.Id && x.Day==DateTimeOffset.Now).ToList();
            this.container = container;
            this.book = book;
            BookTitle.Text = book.Title;
            AuthorName.Text = book.Author;
            TotalPages.Text = book.TotalPages.ToString();
            TodayReadPages.Text = bookPage.todayReadPages.Text;
            
        }
        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            bookPage.Title = BookTitle.Text;
            bookPage.totalPages.Text = TotalPages.Text;
            bookPage.author.Text = AuthorName.Text;
            
            bookPage.todayReadPages.Text = TodayReadPages.Text;
            bookPage.Mark.Text = Mark.Text;
            int readpages = Convert.ToInt32(bookPage.readPages.Text);

            readpages+= Convert.ToInt32(TodayReadPages.Text);
            bookPage.readPages.Text = Convert.ToString(readpages);

            this.Hide();
        }
    }

}
