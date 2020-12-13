using Ninject;
using System;
using System.Windows;
using BookDiary.BLL.Interfaces;
using BookDiary.BLL.DTO;
using BookDiary.DAL.Entities;

namespace BookDiary.PL
{
    /// <summary>
    /// Interaction logic for AddBookPage.xaml
    /// </summary>
    public partial class AddBookPage : Window
    {
        private readonly IKernel container;

        private readonly IBookService bookService;

        private readonly IUserService userService;

        public AddBookPage(IKernel container)
        {
            InitializeComponent();
            bookService = container.Get<IBookService>();
            userService = container.Get<IUserService>();
        }

        private void ButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            var currentUserID = userService.CurrentUser.Id;
            if (UInt32.TryParse(NumberOfPages.Text, out uint totalPages))
            {
                bookService.CreateBook(
                    new BookDTO
                    {
                        UserId = userService.CurrentUser.Id,
                        Title = BookTitle.Text,
                        Author = AuthorName.Text,
                        TotalPages = (int)totalPages,
                        Status = BookStatus.Planned,
                        ReadPages = 0,
                        Review = "",
                    }
                );
            }

            this.Hide();
        }
    }
}
