using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Ninject;
using BookDiary.BLL.Services;
using BookDiary.BLL.Interfaces;
using BookDiary.BLL.Infrastructure;

namespace BookDiary.PL
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IKernel container;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigureContainer();
            ComposeObjects();
            Current.MainWindow.Show();
        }

        private void ConfigureContainer()
        {
            string CS = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            this.container = new StandardKernel(new ServiceModule(CS));
            this.container.Bind<IUserService>().To<UserService>();
        }

        private void ComposeObjects()
        {
            // Window HomePage = new HomePage(container);

            Current.MainWindow = new SignInPage();
        }
    }
}
