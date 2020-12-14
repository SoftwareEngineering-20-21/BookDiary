using Ninject;
using System;
using System.Windows;
using BookDiary.BLL.Interfaces;
using BookDiary.BLL.DTO;
using BookDiary.DAL.Entities;

namespace BookDiary.PL
{
    /// <summary>
    /// Interaction logic for EditBookPage.xaml
    /// </summary>
    public partial class EditBookPage : Window
    {
        BookPage bookPage;
        public EditBookPage(string title, string author, string pages, BookPage bp)
        {
            InitializeComponent();

            BookTitle.Text = title;
            AuthorName.Text = author;
            TotalPages.Text = pages;
            bookPage = bp;
            
        }
        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            bookPage.Title = BookTitle.Text;
            bookPage.totalPages.Text = TotalPages.Text;
            bookPage.author.Text = AuthorName.Text;
            
            bookPage.todayReadPages.Text = TodayReadPages.Text;
            bookPage.Mark.Text = Mark.Text;
            int readpages = Convert.ToInt32(bookPage.readPages.Text);

            readpages+= Convert.ToInt32(TodayReadPages.Text);
            bookPage.readPages.Text = Convert.ToString(readpages);

            this.Hide();
        }
    }

}
