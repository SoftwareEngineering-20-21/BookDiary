using System.Windows.Input;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;
using BookDiary.BLL.DTO;
using BookDiary.BLL.Interfaces;
using Ninject;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Windows.Media.Imaging;

namespace BookDiary.PL
{
    /// <summary>
    /// Interaction logic for NotificationsPage.xaml
    /// </summary>
    public partial class NotificationsPage : Window
    {
        private IKernel container;

        private readonly INotificationService notificationService;
        private readonly IBookService bookService;
        private UserDTO currentUser;

        public NotificationsPage(IKernel container)
        {
            this.container = container;
            this.notificationService = container.Get<INotificationService>();
            this.bookService = container.Get<IBookService>();
            currentUser = container.Get<IUserService>().CurrentUser;

            InitializeComponent();
            if (currentUser != null)
            {
                UserLabel.Content = currentUser.Nickname;
            }

            ShowNotifications();
        }

        private void ShowNotifications()
        {
            IEnumerable<NotificationDTO> Notifications = notificationService.GetNotifications();

            foreach (NotificationDTO notification in Notifications)
            {
                string bookTitle = bookService.GetBooks().Where(x => x.Id == notification.BookId).ToList()[0].Title;
                NotificationIcon notificationIcon = new NotificationIcon(notification, bookTitle);
                NotificationsWrap.Children.Add(new NotificationIcon(notification, bookTitle));
            }
        }

        private void ButtonHome_Click(object sender, RoutedEventArgs e)
        {
            HomePage np = new HomePage(container);
            np.Show();
            this.Hide();
        }

        private void ButtonHome_MouseEnter(object sender, MouseEventArgs e)
        {
            ButtonHome.Background = Brushes.LightBlue;
        }
        private void ButtonHome_MouseLeave(object sender, MouseEventArgs e)
        {
            ButtonHome.Background = Brushes.White;
        }

        private void ButtonStatistics_Click(object sender, RoutedEventArgs e)
        {
            /*
            StatisticsPage sp = new StatisticsPage();
            sp.Show();
            this.Hide();
            */
        }

        private void ButtonStatistics_MouseEnter(object sender, MouseEventArgs e)
        {
            ButtonStatistics.Background = Brushes.LightBlue;
        }

        private void ButtonStatistics_MouseLeave(object sender, MouseEventArgs e)
        {
            ButtonStatistics.Background = Brushes.White;
        }

    }

    public class NotificationIcon : Grid
    {
        StackPanel stackPanel;
        Image image;
        ScrollViewer scrollViewer;
        TextBox textBox;
        Label dateLabel;
        Label titleLabel;
        Border border;
        BookDTO currentBook;

        public NotificationIcon(NotificationDTO notification, string bookTitle)
        {
            RowDefinition rd1 = new RowDefinition();
            RowDefinition rd2 = new RowDefinition();
            RowDefinition rd3 = new RowDefinition();
            RowDefinition rd4 = new RowDefinition();
            ColumnDefinition cd1 = new ColumnDefinition();
            ColumnDefinition cd2 = new ColumnDefinition();
            ColumnDefinition cd3 = new ColumnDefinition();

            rd1.Height = new GridLength(30);
            rd2.Height = new GridLength(10);
            rd3.Height = new GridLength(60);
            rd4.Height = new GridLength(10);
            cd1.Width = new GridLength(500);
            cd2.Width = new GridLength(10);
            cd3.Width = new GridLength(50);
            this.RowDefinitions.Add(rd1);
            this.RowDefinitions.Add(rd2);
            this.RowDefinitions.Add(rd3);
            this.RowDefinitions.Add(rd4);
            this.ColumnDefinitions.Add(cd1);
            this.ColumnDefinitions.Add(cd2);
            this.ColumnDefinitions.Add(cd3);

            stackPanel = new StackPanel();
            image = new Image();
            scrollViewer = new ScrollViewer();
            textBox = new TextBox();
            dateLabel = new Label();
            titleLabel = new Label();

            scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
            scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            textBox.FontSize = 18;
            textBox.FontFamily = new FontFamily("Times New Roman");
            textBox.BorderThickness = new Thickness(0);
            textBox.TextWrapping = TextWrapping.Wrap;
            textBox.Text = notification.Message;
            scrollViewer.Content = textBox;

            Grid.SetRow(scrollViewer, 2);
            Grid.SetColumn(scrollViewer, 0);
            this.Children.Add(scrollViewer);

            titleLabel.FontSize = 20;
            titleLabel.FontFamily = new FontFamily("Times New Roman");
            titleLabel.Content = bookTitle;
            image.Source = new BitmapImage(new Uri("../resource/logo.png", UriKind.Relative));
            image.Height = 30;
            stackPanel.Orientation = Orientation.Horizontal;
            stackPanel.Children.Add(image);
            stackPanel.Children.Add(titleLabel);

            Grid.SetRow(stackPanel, 0);
            Grid.SetColumn(stackPanel, 0);
            this.Children.Add(stackPanel);

            dateLabel.FontSize = 18;
            dateLabel.FontFamily = new FontFamily("Times New Roman");
            dateLabel.Content = notification.Day.DateTime;

            Grid.SetRow(dateLabel, 2);
            Grid.SetColumn(dateLabel, 2);
            this.Children.Add(dateLabel);

            border = new Border();
            border.BorderThickness = new Thickness(2);
            border.CornerRadius = new CornerRadius(10);
            if (notification.IsSeen)
                border.BorderBrush = Brushes.LightGreen;
            else
                border.BorderBrush = Brushes.LightCoral;

            Grid.SetRow(border, 0);
            Grid.SetColumn(border, 0);
            Grid.SetRowSpan(border, 3);
            Grid.SetColumnSpan(border, 3);
            this.Children.Add(border);

        }
    }
}
