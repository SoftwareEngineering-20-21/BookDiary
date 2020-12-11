using BookDiary.BLL.Interfaces;
using BookDiary.BLL.Services;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
    /// Interaction logic for SIgnInPage.xaml
    /// </summary>
    public partial class SignInPage : Window
    {
        private IKernel kernel;
        public SignInPage()
        {
            InitializeComponent();
            var registrations = new NinjectRegistrations();
            this.kernel = new StandardKernel(registrations);
        }


        private void ButtonHomePage_Click(object sender, RoutedEventArgs e)
        {

            HomePage hp = new HomePage(kernel);
            hp.Show();
            this.Hide();

        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            Application.Current.Shutdown();
        }

        private void ButtonSignIn_Click(object sender, RoutedEventArgs e)
        {
            IUserService userService = kernel.Get<IUserService>();
            string email = TextboxEmail.Text;
            string password = TextBoxPassword.Password;

            if (email.Length == 0 || password.Length == 0)
            {
                ErrorLabel.Content = "Any field can not be empty.";
                return;
            }
            if (!userService.IsValidMail(email))
            {
                ErrorLabel.Content = "The email is not a valid email address.";
                return;
            }
            try
            {
                HashService Hash = new HashService();
                var user = userService.Login(email, password);
                HomePage hp = new HomePage(kernel);
                hp.Show();
                this.Hide();
            }
            catch (ArgumentException exc)
            {
                ErrorLabel.Content = exc.Message;
            }
        }

        private void ButtonSignUp_Click(object sender, RoutedEventArgs e)
        {
            SignUpPage hp = new SignUpPage(kernel);
            hp.Show();
            this.Hide();
        }
    }
}
