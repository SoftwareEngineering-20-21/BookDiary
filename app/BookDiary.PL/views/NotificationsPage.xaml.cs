// <copyright file="NotificationsPage.xaml.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BookDiary.PL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using BookDiary.BLL.DTO;
    using BookDiary.BLL.Interfaces;
    using Ninject;

    /// <summary>
    /// Interaction logic for NotificationsPage.xaml.
    /// </summary>
    public partial class NotificationsPage : Window
    {
        private readonly INotificationService notificationService;
        private readonly IBookService bookService;
        private readonly List<BookDTO> books;
        private UserDTO currentUser;
        private IKernel container;

#pragma warning disable SA1614 // Element parameter documentation should have text
                              /// <summary>
                              /// Initializes a new instance of the <see cref="NotificationsPage"/> class.
                              /// </summary>
                              /// <param name="container"></param>
        public NotificationsPage(IKernel container)
#pragma warning restore SA1614 // Element parameter documentation should have text
        {
            this.container = container;
            this.notificationService = container.Get<INotificationService>();
            this.bookService = container.Get<IBookService>();
            this.currentUser = container.Get<IUserService>().CurrentUser;
            this.books = this.bookService.GetBooks().Where(x => x.UserId == this.currentUser.Id).ToList();

            this.InitializeComponent();
            if (this.currentUser != null)
            {
                this.UserLabel.Content = this.currentUser.Nickname;
            }

            this.ShowNotifications();
        }

        private void ShowNotifications()
        {
            IEnumerable<NotificationDTO> notifications = new List<NotificationDTO>();
            foreach (BookDTO book in this.books)
            {
                notifications = notifications.Concat<NotificationDTO>(this.notificationService.GetNotifications().Where(x => x.BookId == book.Id).ToList());
            }

            foreach (NotificationDTO notification in notifications)
            {
                string bookTitle = this.bookService.GetBooks().Where(x => x.Id == notification.BookId).ToList()[0].Title;
                NotificationIcon notificationIcon = new NotificationIcon(notification, bookTitle);
                this.NotificationsWrap.Children.Add(new NotificationIcon(notification, bookTitle));
            }
        }

        private void ButtonHome_Click(object sender, RoutedEventArgs e)
        {
            HomePage np = new HomePage(this.container);
            np.Show();
            this.Hide();
        }

        private void ButtonHome_MouseEnter(object sender, MouseEventArgs e)
        {
            this.ButtonHome.Background = Brushes.LightBlue;
        }

        private void ButtonHome_MouseLeave(object sender, MouseEventArgs e)
        {
            this.ButtonHome.Background = Brushes.White;
        }

        private void ButtonStatistics_Click(object sender, RoutedEventArgs e)
        {
            StatisticsPage sp = new StatisticsPage(container);
            sp.Show();
            this.Hide();
        }

        private void ButtonStatistics_MouseEnter(object sender, MouseEventArgs e)
        {
            this.ButtonStatistics.Background = Brushes.LightBlue;
        }

        private void ButtonStatistics_MouseLeave(object sender, MouseEventArgs e)
        {
            this.ButtonStatistics.Background = Brushes.White;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationIcon"/> class.
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="bookTitle"></param>
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
            cd3.Width = new GridLength(100);
            this.RowDefinitions.Add(rd1);
            this.RowDefinitions.Add(rd2);
            this.RowDefinitions.Add(rd3);
            this.RowDefinitions.Add(rd4);
            this.ColumnDefinitions.Add(cd1);
            this.ColumnDefinitions.Add(cd2);
            this.ColumnDefinitions.Add(cd3);

            this.stackPanel = new StackPanel();
            this.image = new Image();
            this.scrollViewer = new ScrollViewer();
            this.textBox = new TextBox();
            this.dateLabel = new Label();
            this.titleLabel = new Label();

            this.scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
            this.scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            this.textBox.FontSize = 18;
            this.textBox.FontFamily = new FontFamily("Times New Roman");
            this.textBox.BorderThickness = new Thickness(0);
            this.textBox.TextWrapping = TextWrapping.Wrap;
            this.textBox.Text = notification.Message;
            this.scrollViewer.Content = this.textBox;

            Grid.SetRow(this.scrollViewer, 2);
            Grid.SetColumn(this.scrollViewer, 0);
            this.Children.Add(this.scrollViewer);

            this.titleLabel.FontSize = 20;
            this.titleLabel.FontFamily = new FontFamily("Times New Roman");
            this.titleLabel.Content = bookTitle;
            this.image.Source = new BitmapImage(new Uri("../resource/logo.png", UriKind.Relative));
            this.image.Height = 30;
            this.stackPanel.Orientation = Orientation.Horizontal;
            this.stackPanel.Children.Add(this.image);
            this.stackPanel.Children.Add(this.titleLabel);

            Grid.SetRow(this.stackPanel, 0);
            Grid.SetColumn(this.stackPanel, 0);
            this.Children.Add(this.stackPanel);

            this.dateLabel.FontSize = 18;
            this.dateLabel.FontFamily = new FontFamily("Times New Roman");
            this.dateLabel.Content = notification.Day.DateTime;

            Grid.SetRow(this.dateLabel, 2);
            Grid.SetColumn(this.dateLabel, 2);
            this.Children.Add(this.dateLabel);

            this.border = new Border();
            this.border.BorderThickness = new Thickness(2);
            this.border.CornerRadius = new CornerRadius(10);
            if (notification.IsSeen)
            {
                this.border.BorderBrush = Brushes.LightGreen;
            }
            else
            {
                this.border.BorderBrush = Brushes.LightCoral;
            }

            Grid.SetRow(this.border, 0);
            Grid.SetColumn(this.border, 0);
            Grid.SetRowSpan(this.border, 3);
            Grid.SetColumnSpan(this.border, 3);
            this.Children.Add(this.border);
        }
    }
}
