using Ninject;
using System;
using System.Windows;
using BookDiary.BLL.Interfaces;
using BookDiary.BLL.DTO;
using System.Windows.Controls;

namespace BookDiary.PL
{
    /// <summary>
    /// Interaction logic for BookNotificationPage.xaml
    /// </summary>
    public partial class BookNotificationPage : Window
    {
        private IKernel container;

        private BookDTO book;

        private readonly INotificationService notificationService;

        public BookNotificationPage(IKernel container, BookDTO book)
        {
            this.container = container;
            this.notificationService = container.Get<INotificationService>();
            this.book = book;

            InitializeComponent();
            LableBookTitle.Content = book.Title;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int bookId = book.Id;
            string message = TextBoxMessage.Text;
            DateTime? calendarDay = Calendar.SelectedDate;
            try 
            {
                if (calendarDay == null)
                {
                    ErrorLabel.Content = "Date is not setted";
                    throw new Exception("Date is not setted");
                }
                // Notification will be sent at 12 pm
                DateTimeOffset day = new DateTimeOffset((DateTime)calendarDay, new TimeSpan(12, 0, 0));
                
                notificationService.CreateNotification(
                   new NotificationDTO
                   {
                       Message = message,
                       Day = day,
                       BookId = bookId
                   }
                );
                this.Close();
            }
            catch(Exception ex)
            {
                ErrorLabel.Content = ex.Message;
            }
        }
    }
}