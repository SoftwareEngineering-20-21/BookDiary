using BookDiary.BLL.DTO;
using BookDiary.BLL.Interfaces;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace BookDiary.PL
{
    public enum TimePeriod
    {
        Week,
        Month,
        Year,
        Whole
    }

    /// <summary>
    /// Interaction logic for StatisticsPage.xaml
    /// </summary>
    public partial class StatisticsPage : Window
    {
        TimePeriod timePeriod;
        IEnumerable<StatisticDTO> StatisticsWeek;
        IEnumerable<StatisticDTO> StatisticsMonth;
        IEnumerable<StatisticDTO> StatisticsYear;
        IEnumerable<StatisticDTO> StatisticsWhole;
        KeyValuePair<string, int>[] DataWeek;
        KeyValuePair<string, int>[] DataMonth;
        KeyValuePair<string, int>[] DataYear;
        KeyValuePair<string, int>[] DataWhole;
        int Nweek, Nmonth, Nyear, Nwhole;

        private IKernel container;

        private readonly IStatisticService statisticService;
        private readonly IBookService bookService;
        private readonly List<BookDTO> Books;
        private UserDTO currentUser;

        public StatisticsPage(IKernel container)
        {
            this.container = container;
            this.statisticService = container.Get<IStatisticService>();
            this.bookService = container.Get<IBookService>();
            currentUser = container.Get<IUserService>().CurrentUser;
            Books = bookService.GetBooks().Where(x => x.UserId == currentUser.Id).ToList();

            InitializeComponent();

            if (currentUser != null)
            {
                UserLabel.Content = currentUser.Nickname;
            }

            StatisticsWhole = new List<StatisticDTO>();
            StatisticsWeek = new List<StatisticDTO>();
            StatisticsMonth = new List<StatisticDTO>();
            StatisticsYear = new List<StatisticDTO>();
            ShowStatistics();
        }

        private void ShowStatistics()
        {
            foreach (BookDTO book in Books)
            {
                StatisticsWhole = StatisticsWhole.Concat<StatisticDTO>(statisticService.GetStatistics().Where(x => x.BookId == book.Id).ToList());
                StatisticsWeek = StatisticsWeek.Concat<StatisticDTO>(statisticService.GetStatistics().Where(x => x.BookId == book.Id && x.Day > DateTime.Now.AddDays(-7)).ToList());
                StatisticsMonth = StatisticsMonth.Concat<StatisticDTO>(statisticService.GetStatistics().Where(x => x.BookId == book.Id && x.Day > DateTime.Now.AddDays(-30)).ToList());
                StatisticsYear = StatisticsYear.Concat<StatisticDTO>(statisticService.GetStatistics().Where(x => x.BookId == book.Id && x.Day > DateTime.Now.AddMonths(-12)).ToList());
            }

            StatisticsWhole = StatisticsWhole.OrderBy(x => x.Day).ToList();
            StatisticsWeek = StatisticsWeek.OrderBy(x => x.Day).ToList();
            StatisticsMonth = StatisticsMonth.OrderBy(x => x.Day).ToList();
            StatisticsYear = StatisticsYear.OrderBy(x => x.Day).ToList();

            Nweek = 7;
            Nmonth = 30;
            Nyear = 12;
            Nwhole = StatisticsWhole.Last<StatisticDTO>().Day.Year * 12  + StatisticsWhole.Last<StatisticDTO>().Day.Month - StatisticsWhole.First<StatisticDTO>().Day.Year * 12 - StatisticsWhole.First<StatisticDTO>().Day.Month + 1;

            DataWhole = new KeyValuePair<string, int>[Nwhole];
            DataWeek = new KeyValuePair<string, int>[Nweek];
            DataMonth = new KeyValuePair<string, int>[Nmonth];
            DataYear = new KeyValuePair<string, int>[Nyear];

            for (int i = 0; i < Nweek; i++)
            {
                String dayOfWeek = DateTime.Now.AddDays(-Nweek + 1 + i).DayOfWeek.ToString();
                int pages = 0;
                foreach(StatisticDTO statistic in StatisticsWeek)
                {
                    if (statistic.Day.DayOfWeek.ToString() == dayOfWeek) pages += statistic.NewPages;
                }
                DataWeek[i] = new KeyValuePair<string, int>(dayOfWeek, pages);
            }

            for (int i = 0; i < Nmonth; i++)
            {
                String dayOfMonth = DateTime.Now.AddDays(-Nmonth + 1 + i).ToShortDateString();
                int pages = 0;
                foreach (StatisticDTO statistic in StatisticsMonth)
                {
                    if (statistic.Day.DateTime.ToShortDateString() == dayOfMonth) pages += statistic.NewPages;
                }
                DataMonth[i] = new KeyValuePair<string, int>(dayOfMonth, pages);
            }

            for (int i = 0; i < Nyear; i++)
            {
                String monthOfYear = DateTime.Now.AddMonths(-Nyear + 1 + i).ToString("MMMM");
                int pages = 0;
                foreach (StatisticDTO statistic in StatisticsYear)
                {
                    if (statistic.Day.ToString("MMMM") == monthOfYear) pages += statistic.NewPages;
                }
                DataYear[i] = new KeyValuePair<string, int>(monthOfYear, pages);
            }

            for (int i = 0; i < Nwhole; i++)
            {
                DateTime time = DateTime.Now.AddMonths(-Nwhole + 1 + i);
                int pages = 0;
                foreach (StatisticDTO statistic in StatisticsWhole)
                {
                    if (statistic.Day.Year == time.Year && statistic.Day.Month == time.Month) pages += statistic.NewPages;
                }
                DataWhole[i] = new KeyValuePair<string, int>(time.ToString("MM'/'yyyy"), pages);
            }

            timePeriod = TimePeriod.Week;
            ButtonWeek.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
        }

        private void ButtonHome_Click(object sender, RoutedEventArgs e)
        {
            HomePage np = new HomePage(container);
            np.Show();
            this.Hide();
        }

        private void ButtonNotifications_Click(object sender, RoutedEventArgs e)
        {
            NotificationsPage np = new NotificationsPage(container);
            np.Show();
            this.Hide();
        }

        private void ButtonWeek_Click(object sender, RoutedEventArgs e)
        {
            timePeriod = TimePeriod.Week;
            ButtonWeek.Background = Brushes.LightGray;
            ButtonMonth.Background = Brushes.White;
            ButtonYear.Background = Brushes.White;
            ButtonWhole.Background = Brushes.White;

            ((ColumnSeries)Chart).ItemsSource = DataWeek;
        }

        private void ButtonMonth_Click(object sender, RoutedEventArgs e)
        {
            timePeriod = TimePeriod.Month;
            ButtonWeek.Background = Brushes.White;
            ButtonMonth.Background = Brushes.LightGray;
            ButtonYear.Background = Brushes.White;
            ButtonWhole.Background = Brushes.White;

            ((ColumnSeries)Chart).ItemsSource = DataMonth;
        }

        private void ButtonYear_Click(object sender, RoutedEventArgs e)
        {
            timePeriod = TimePeriod.Year;
            ButtonWeek.Background = Brushes.White;
            ButtonMonth.Background = Brushes.White;
            ButtonYear.Background = Brushes.LightGray;
            ButtonWhole.Background = Brushes.White;

            ((ColumnSeries)Chart).ItemsSource = DataYear;
        }

        private void ButtonWhole_Click(object sender, RoutedEventArgs e)
        {
            timePeriod = TimePeriod.Whole;
            ButtonWeek.Background = Brushes.White;
            ButtonMonth.Background = Brushes.White;
            ButtonYear.Background = Brushes.White;
            ButtonWhole.Background = Brushes.LightGray;

            ((ColumnSeries)Chart).ItemsSource = DataWhole;
        }

        private void ButtonHome_MouseEnter(object sender, MouseEventArgs e)
        {
            ButtonHome.Background = Brushes.LightBlue;
        }

        private void ButtonHome_MouseLeave(object sender, MouseEventArgs e)
        {
            ButtonHome.Background = Brushes.White;
        }

        private void ButtonNotifications_MouseEnter(object sender, MouseEventArgs e)
        {
            ButtonNotifications.Background = Brushes.LightBlue;
        }

        private void ButtonNotifications_MouseLeave(object sender, MouseEventArgs e)
        {
            ButtonNotifications.Background = Brushes.White;
        }

        private void ButtonWeek_MouseEnter(object sender, MouseEventArgs e)
        {
            if (timePeriod != TimePeriod.Week)
                ButtonWeek.Background = Brushes.LightBlue;
        }

        private void ButtonWeek_MouseLeave(object sender, MouseEventArgs e)
        {
            if (timePeriod != TimePeriod.Week)
                ButtonWeek.Background = Brushes.White;
        }

        private void ButtonMonth_MouseEnter(object sender, MouseEventArgs e)
        {
            if (timePeriod != TimePeriod.Month)
                ButtonMonth.Background = Brushes.LightBlue;
        }

        private void ButtonMonth_MouseLeave(object sender, MouseEventArgs e)
        {
            if (timePeriod != TimePeriod.Month)
                ButtonMonth.Background = Brushes.White;
        }

        private void ButtonYear_MouseEnter(object sender, MouseEventArgs e)
        {
            if (timePeriod != TimePeriod.Year)
                ButtonYear.Background = Brushes.LightBlue;
        }

        private void ButtonYear_MouseLeave(object sender, MouseEventArgs e)
        {
            if (timePeriod != TimePeriod.Year)
                ButtonYear.Background = Brushes.White;
        }

        private void ButtonWhole_MouseEnter(object sender, MouseEventArgs e)
        {
            if (timePeriod != TimePeriod.Whole)
                ButtonWhole.Background = Brushes.LightBlue;
        }

        private void ButtonWhole_MouseLeave(object sender, MouseEventArgs e)
        {
            if (timePeriod != TimePeriod.Whole)
                ButtonWhole.Background = Brushes.White;
        }
    }
}
