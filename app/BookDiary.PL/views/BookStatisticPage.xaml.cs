using BookDiary.BLL.DTO;
using BookDiary.BLL.Interfaces;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls.DataVisualization;
using System.Windows.Controls.DataVisualization.Charting;

namespace BookDiary.PL
{
    /// <summary>
    /// Interaction logic for BookStatisticPage.xaml
    /// </summary>
    public partial class BookStatisticPage : Window
    {
        private IKernel container;

        private BookDTO book;

        private readonly IStatisticService statisticService;
        private List<StatisticDTO> Statistic;

        public BookStatisticPage(IKernel container, BookDTO book)
        {
            this.container = container;
            this.statisticService = container.Get<IStatisticService>();
            this.book = book;
            this.Statistic = statisticService.GetStatistics().Where(x => x.BookId == book.Id).ToList();

            InitializeComponent();
            LabelBookTitle.Content = book.Title;

            ShowStatistic();
        }

        private void ShowStatistic()
        {
            Statistic.OrderBy(x => x.Day);
            KeyValuePair<string, int>[] Data = new KeyValuePair<string, int> [Statistic.Count()];
            int i = 0;
            foreach(StatisticDTO statistic in Statistic)
            {
                Data[i] = new KeyValuePair<string, int>(statistic.Day.DateTime.ToShortDateString(), statistic.NewPages);
                i++;
            }

            ((ColumnSeries)Chart).ItemsSource = Data;
        }
    }
}
