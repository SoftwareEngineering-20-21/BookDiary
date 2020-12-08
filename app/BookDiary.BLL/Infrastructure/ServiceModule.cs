using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookDiary.DAL.Interfaces;
using BookDiary.DAL.Repositories;
using BookDiary.BLL.Interfaces;
using BookDiary.BLL.Services;

namespace BookDiary.BLL.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        private string connectionString;
        public ServiceModule(string connection)
        {
            connectionString = connection;
        }
        public override void Load()
        {
            Bind<IUnitOfWork>().To<EFUnitOfWork>().WithConstructorArgument(connectionString);
            Bind<IUserService>().To<UserService>();
            Bind<IBookService>().To<BookService>();
            Bind<IStatisticService>().To<StatisticService>();
            Bind<INotificationService>().To<NotificationService>();
            Bind<IHashService>().To<HashService>();
        }
    }
}
