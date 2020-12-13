using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BookDiary.DAL.Entities;
using BookDiary.BLL.Interfaces;
using BookDiary.BLL.DTO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
                    ErrorLabel.Content = "Date is nit setted";
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