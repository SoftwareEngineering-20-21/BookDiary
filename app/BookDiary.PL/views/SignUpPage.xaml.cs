using System;
using Ninject;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using BookDiary.BLL.Interfaces;

namespace BookDiary.PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SignUpPage : Window
    {
        private IKernel kernel;
        public SignUpPage(IKernel kernel)
        {
            InitializeComponent();
            this.kernel = kernel;
        }


        private void ButtonHomePage_Click(object sender, RoutedEventArgs e)
        {

            HomePage hp = new HomePage();
            hp.Show();
            this.Hide();
        }


        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            Application.Current.Shutdown();
        }

        private void ButtonSignUp_Click(object sender, RoutedEventArgs e)
        {
            IUserService userService = kernel.Get<IUserService>();
            string nickName = TextBoxNickName.Text;
            string fullName = TextBoxFullName.Text;
            string email = TextBoxEmail.Text;
            string password = TextBoxPassword.Password;
            string confirmPassword = TextBoxConfirmPassword.Password;

            if (nickName.Length == 0 || fullName.Length == 0 || email.Length == 0 || password.Length == 0 || confirmPassword.Length == 0)
            {
                ErrorLabel.Content = "Any field can not be empty.";
                return;
            }
            if (!userService.IsValidMail(email))
            {
                ErrorLabel.Content = "The email is not a valid email address.";
                return;
            }
            if (password != confirmPassword)
            {
                ErrorLabel.Content = "Password doesn't match.";
                return;
            }
            try
            {
                var user = userService.SignUp(nickName, fullName, email, password);
                HomePage hp = new HomePage(kernel);
                hp.Show();
                Close();
            }
            catch (ArgumentException exc)
            {
                ErrorLabel.Content = exc.Message;
            }
        }
    }
}
