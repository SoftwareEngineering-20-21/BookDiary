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

        public BookNotificationPage(IKernel container, BookDTO book)
        {
            this.container = container;
            this.book = book;

            InitializeComponent();
        }
    }
}