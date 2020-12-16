using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using BookDiary.DAL.Entities;
using BookDiary.BLL.Interfaces;
using BookDiary.BLL.DTO;
using Ninject;

namespace BookDiary.PL
{
    public enum Status
    { 
        All,
        Planned,
        InProgress,
        Comleted
    }

    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Window
    {
        Status bookListStatus;
        List<BookDTO> BooksAll;
        List<BookDTO> BooksInProgress;
        List<BookDTO> BooksPlanned;
        List<BookDTO> BooksCompleted;

        private IKernel container;

        private IBookService bookService;

        UserDTO currentUser;

        public HomePage(IKernel container)
        {
            this.container = container;
            currentUser = container.Get<IUserService>().CurrentUser;
            bookService = container.Get<IBookService>();

            InitializeComponent();
            if (currentUser != null)
            {
                UserLabel.Content = currentUser.Nickname;
            }

            ShowBooks();
        }

        private void ShowBooks()
        {
            bookListStatus = Status.All;

            IEnumerable<BookDTO> Books = bookService.GetBooks();
            BooksAll = Books.Where(x => x.UserId == currentUser.Id).ToList();
            BooksInProgress = BooksAll.Where(x => x.Status == BookStatus.InProgress).ToList();
            BooksPlanned = BooksAll.Where(x => x.Status == BookStatus.Planned).ToList();
            BooksCompleted = BooksAll.Where(x => x.Status == BookStatus.Completed).ToList();

            ButtonAll.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
        }

        private void ShowBookPage(BookDTO book)
        {
            BookPage bp = new BookPage(container, book);
            bp.Show();
            bp.Closed += new EventHandler((object s, EventArgs ee) => { ShowBooks(); });
        }

        private void ButtonAll_Click(object sender, RoutedEventArgs e)
        {
            bookListStatus = Status.All;
            ButtonAll.Background = Brushes.LightGray;
            ButtonPlanned.Background = Brushes.White;
            ButtonInProgress.Background = Brushes.White;
            ButtonCompleted.Background = Brushes.White;

            booksWrap.Children.Clear();
            foreach (BookDTO book in BooksAll)
            {
                BookIcon bi = new BookIcon(book);
                bi.Click += (x, y) => ShowBookPage(book);
                booksWrap.Children.Add(bi);
            }
        }

        private void ButtonPlanned_Click(object sender, RoutedEventArgs e)
        {
            bookListStatus = Status.Planned;
            ButtonAll.Background = Brushes.White;
            ButtonPlanned.Background = Brushes.LightGray;
            ButtonInProgress.Background = Brushes.White;
            ButtonCompleted.Background = Brushes.White;

            booksWrap.Children.Clear();
            foreach (BookDTO book in BooksPlanned)
            {
                BookIcon bi = new BookIcon(book);
                bi.Click += (x, y) => ShowBookPage(book);
                booksWrap.Children.Add(bi);
            }
        }

        private void ButtonInProgress_Click(object sender, RoutedEventArgs e)
        {
            bookListStatus = Status.InProgress;
            ButtonAll.Background = Brushes.White;
            ButtonPlanned.Background = Brushes.White;
            ButtonInProgress.Background = Brushes.LightGray;
            ButtonCompleted.Background = Brushes.White;

            booksWrap.Children.Clear();
            foreach (BookDTO book in BooksInProgress)
            {
                BookIcon bi = new BookIcon(book);
                bi.Click += (x, y) => ShowBookPage(book);
                booksWrap.Children.Add(bi);
            }
        }

        private void ButtonCompleted_Click(object sender, RoutedEventArgs e)
        {
            bookListStatus = Status.Comleted;
            ButtonAll.Background = Brushes.White;
            ButtonPlanned.Background = Brushes.White;
            ButtonInProgress.Background = Brushes.White;
            ButtonCompleted.Background = Brushes.LightGray;

            booksWrap.Children.Clear();
            foreach (BookDTO book in BooksCompleted)
            {
                BookIcon bi = new BookIcon(book);
                bi.Click += (x, y) => ShowBookPage(book);
                booksWrap.Children.Add(bi);
            }
        }

        private void ButtonNotifications_Click(object sender, RoutedEventArgs e)
        {
            NotificationsPage np = new NotificationsPage(container);
            np.Show();
            this.Hide();
        }

        private void ButtonStatistics_Click(object sender, RoutedEventArgs e)
        {
            StatisticsPage sp = new StatisticsPage(container);
            sp.Show();
            this.Hide();
        }


        private void ButtonSignIn_Click(object sender, RoutedEventArgs e)
        {

            SignInPage sp = new SignInPage(container);
            sp.Show();
            this.Hide();
        }

        private void ButtonSignUp_Click(object sender, RoutedEventArgs e)
        {
            SignUpPage sp = new SignUpPage(container);
            sp.Show();
            this.Hide();
        }      

        private void AddBook_Click(object sender, RoutedEventArgs e)
        {
            AddBookPage ab = new AddBookPage(container);
            ab.Show();
            ab.Closed += new EventHandler((object s, EventArgs ee) => { ShowBooks(); });
        }

        private void SearchTextBox_DragEnter(object sender, DragEventArgs e)
        {
            string titleSearched = SearchTextBox.Text;
            foreach(BookDTO book in BooksAll)
                if (book.Title == titleSearched)
                {
                    ShowBookPage(book);
                    return;
                }
            MessageBox.Show("Book " + titleSearched + " is not found");
        }

        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string titleSearched = SearchTextBox.Text;
                foreach (BookDTO book in BooksAll)
                    if (book.Title == titleSearched)
                    {
                        ShowBookPage(book);
                        return;
                    }
                MessageBox.Show("Book " + titleSearched + " is not found");
            }
        }

        private void ButtonAll_MouseEnter(object sender, MouseEventArgs e)
        {
            if (bookListStatus != Status.All)
                ButtonAll.Background = Brushes.LightBlue;
        }

        private void ButtonAll_MouseLeave(object sender, MouseEventArgs e)
        {
            if (bookListStatus != Status.All)
                ButtonAll.Background = Brushes.White;
        }

        private void ButtonPlanned_MouseEnter(object sender, MouseEventArgs e)
        {
            if (bookListStatus != Status.Planned)
                ButtonPlanned.Background = Brushes.LightBlue;
        }

        private void ButtonPlanned_MouseLeave(object sender, MouseEventArgs e)
        {
            if (bookListStatus != Status.Planned)
                ButtonPlanned.Background = Brushes.White;
        }

        private void ButtonInProgress_MouseEnter(object sender, MouseEventArgs e)
        {
            if (bookListStatus != Status.InProgress)
                ButtonInProgress.Background = Brushes.LightBlue;
        }

        private void ButtonInProgress_MouseLeave(object sender, MouseEventArgs e)
        {
            if (bookListStatus != Status.InProgress)
                ButtonInProgress.Background = Brushes.White;
        }

        private void ButtonCompleted_MouseEnter(object sender, MouseEventArgs e)
        {
            if (bookListStatus != Status.Comleted)
                ButtonCompleted.Background = Brushes.LightBlue;
        }

        private void ButtonCompleted_MouseLeave(object sender, MouseEventArgs e)
        {
            if (bookListStatus != Status.Comleted)
                ButtonCompleted.Background = Brushes.White;
        }

        private void ButtonNotifications_MouseEnter(object sender, MouseEventArgs e)
        {
            ButtonNotifications.Background = Brushes.LightBlue;
        }

        private void ButtonNotifications_MouseLeave(object sender, MouseEventArgs e)
        {
            ButtonNotifications.Background = Brushes.White;
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

    public class BookIcon: Button
    {
        private Image image;
        private TextBlock textBlock;
        private Grid grid;
        private StackPanel imageStack;

        public BookIcon(BookDTO book)
        {
            this.Width = 190;
            this.Height = 250;
            this.Background = Brushes.White;
            this.VerticalAlignment = VerticalAlignment.Top;
            this.HorizontalAlignment = HorizontalAlignment.Left;

            grid = new Grid();
            RowDefinition rd1 = new RowDefinition();
            RowDefinition rd2 = new RowDefinition();
            RowDefinition rd3 = new RowDefinition();
            RowDefinition rd4 = new RowDefinition();
            RowDefinition rd5 = new RowDefinition();
            ColumnDefinition cd1 = new ColumnDefinition();
            ColumnDefinition cd2 = new ColumnDefinition();
            ColumnDefinition cd3 = new ColumnDefinition();
            ColumnDefinition cd4 = new ColumnDefinition();

            rd1.Height = new GridLength(10);
            rd2.Height = new GridLength(190);
            rd3.Height = new GridLength(10);
            rd4.Height = new GridLength(30);
            rd5.Height = new GridLength(10);
            cd1.Width = new GridLength(10);
            cd2.Width = new GridLength(120);
            cd3.Width = new GridLength(50);
            cd4.Width = new GridLength(10);
            grid.RowDefinitions.Add(rd1);
            grid.RowDefinitions.Add(rd2);
            grid.RowDefinitions.Add(rd3);
            grid.RowDefinitions.Add(rd4);
            grid.RowDefinitions.Add(rd5);
            grid.ColumnDefinitions.Add(cd1);
            grid.ColumnDefinitions.Add(cd2);
            grid.ColumnDefinitions.Add(cd3);
            grid.ColumnDefinitions.Add(cd4);

            image = new Image();
            image.Source = new BitmapImage(new Uri("../resource/7.png", UriKind.Relative));

            Grid.SetRow(image, 1);
            Grid.SetColumn(image, 1);
            Grid.SetColumnSpan(image, 2);
            grid.Children.Add(image);
           

            textBlock = new TextBlock();
            textBlock.Text = book.Title;
            textBlock.FontFamily = new FontFamily("Times New Roman");
            textBlock.FontSize = 16;
            textBlock.VerticalAlignment = VerticalAlignment.Center;
            textBlock.HorizontalAlignment = HorizontalAlignment.Left;

            Grid.SetRow(textBlock, 3);
            Grid.SetColumn(textBlock, 1);
            grid.Children.Add(textBlock);

            imageStack = new StackPanel();
            imageStack.Orientation = Orientation.Horizontal;
            for (int i = 0; i < book.Mark; i++)
            {
                Image star = new Image();
                star.Source = new BitmapImage(new Uri("../resource/star-icon.png", UriKind.Relative));
                star.Width = 10;
                imageStack.Children.Add(star);
            }
            Grid.SetRow(imageStack, 2);
            Grid.SetColumn(imageStack, 2);
            grid.Children.Add(imageStack);

            if (book.Status == BookStatus.Completed)
            {
                Image isCompleted = new Image();
                isCompleted.Source = new BitmapImage(new Uri("../resource/done-icon-3.png", UriKind.Relative));

                Grid.SetRow(isCompleted, 3);
                Grid.SetColumn(isCompleted, 2);
                grid.Children.Add(isCompleted);
            }
            this.AddChild(grid);
        }
    }
}
