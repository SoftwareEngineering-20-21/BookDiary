using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace BookDiary.Pl
{
    public enum Status
    { 
        All,
        ToRead,
        InProgress,
        Done
    }

    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Window
    {
        Status status;

        public HomePage()
        {
            InitializeComponent();
            status = Status.All;
            ButtonAll.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
        }

        private void ButtonAll_Click(object sender, RoutedEventArgs e)
        {
            status = Status.All;
            ButtonAll.Background = Brushes.LightGray;
            ButtonToRead.Background = Brushes.White;
            ButtonInProgress.Background = Brushes.White;
            ButtonDone.Background = Brushes.White;
        }

        private void ButtonAll_MouseEnter(object sender, MouseEventArgs e)
        {
            if (status != Status.All)
                ButtonAll.Background = Brushes.LightBlue;
        }

        private void ButtonAll_MouseLeave(object sender, MouseEventArgs e)
        {
            if (status != Status.All)
                ButtonAll.Background = Brushes.White;
        }

        private void ButtonToRead_Click(object sender, RoutedEventArgs e)
        {
            status = Status.ToRead;
            ButtonAll.Background = Brushes.White;
            ButtonToRead.Background = Brushes.LightGray;
            ButtonInProgress.Background = Brushes.White;
            ButtonDone.Background = Brushes.White;
        }

        private void ButtonToRead_MouseEnter(object sender, MouseEventArgs e)
        {
            if (status != Status.ToRead)
                ButtonToRead.Background = Brushes.LightBlue;
        }

        private void ButtonToRead_MouseLeave(object sender, MouseEventArgs e)
        {
            if (status != Status.ToRead)
                ButtonToRead.Background = Brushes.White;
        }

        private void ButtonInProgress_Click(object sender, RoutedEventArgs e)
        {
            status = Status.InProgress;
            ButtonAll.Background = Brushes.White;
            ButtonToRead.Background = Brushes.White;
            ButtonInProgress.Background = Brushes.LightGray;
            ButtonDone.Background = Brushes.White;
        }

        private void ButtonInProgress_MouseEnter(object sender, MouseEventArgs e)
        {
            if (status != Status.InProgress)
                ButtonInProgress.Background = Brushes.LightBlue;
        }

        private void ButtonInProgress_MouseLeave(object sender, MouseEventArgs e)
        {
            if (status != Status.InProgress)
                ButtonInProgress.Background = Brushes.White;
        }

        private void ButtonDone_Click(object sender, RoutedEventArgs e)
        {
            status = Status.Done;
            ButtonAll.Background = Brushes.White;
            ButtonToRead.Background = Brushes.White;
            ButtonInProgress.Background = Brushes.White;
            ButtonDone.Background = Brushes.LightGray;
        }

        private void ButtonDone_MouseEnter(object sender, MouseEventArgs e)
        {
            if (status != Status.Done)
                ButtonDone.Background = Brushes.LightBlue;
        }

        private void ButtonDone_MouseLeave(object sender, MouseEventArgs e)
        {
            if (status != Status.Done)
                ButtonDone.Background = Brushes.White;
        }

        private void ButtonSignIn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
