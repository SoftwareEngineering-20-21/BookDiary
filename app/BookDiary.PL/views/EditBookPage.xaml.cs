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

        public EditBookPage(IKernel container, BookDTO book, BookPage bp)
        {
            InitializeComponent();
            bookPage = bp;
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
            IEnumerable<StatisticDTO> statistics = statisticService.GetStatistics();
            statistic = statistics.Where(x => x.BookId == book.Id && x.Day==DateTimeOffset.Now.Date).ToList();
            this.container = container;
            this.book = book;
            BookTitle.Text = book.Title;
            AuthorName.Text = book.Author;
            TotalPages.Text = book.TotalPages.ToString();
            TodayReadPages.Text = bookPage.todayReadPages.Text;
            
        }
        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            StatisticDTO curStat = statistic.ElementAt<StatisticDTO>(0);
            bookPage.Title = BookTitle.Text;
            bookPage.totalPages.Text = TotalPages.Text;
            bookPage.author.Text = AuthorName.Text;
            
            bookPage.todayReadPages.Text = TodayReadPages.Text;
            bookPage.Mark.Text = Mark.Text;

            statisticService.UpdateStatistic(new StatisticDTO
            {
                Id = curStat.Id,
                Day = curStat.Day,
                OldPages = 0,
                NewPages = Convert.ToInt32(TodayReadPages.Text)
            });
            int readpages = Convert.ToInt32(bookPage.readPages.Text);

            readpages+= Convert.ToInt32(TodayReadPages.Text);
            bookPage.readPages.Text = Convert.ToString(readpages);
            bookService.UpdateBook(new BookDTO{
                Id =book.Id,
                Status = book.Status,
                Title = BookTitle.Text,
                Author = AuthorName.Text,
                TotalPages = Convert.ToInt32(TotalPages.Text),
                ReadPages = readpages,
                Review = book.Review,
                Mark = Convert.ToInt32(Mark.Text)
            });
            this.Hide();
        }
    }

}
