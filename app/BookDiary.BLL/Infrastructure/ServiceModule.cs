using Ninject.Modules;
using BookDiary.DAL.EF;
using BookDiary.DAL.Entities;
using BookDiary.DAL.Interfaces;
using BookDiary.DAL.Repositories;
using BookDiary.BLL.Interfaces;
using BookDiary.BLL.Services;

namespace BookDiary.BLL.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        private string connectionString;

        public ServiceModule(string connection) : base()
        {
            connectionString = connection;
        }

        public override void Load()
        {
            Bind<IUnitOfWork>().To<EFUnitOfWork>().InSingletonScope();
            Bind<AppDbContext>().ToSelf().InSingletonScope().WithConstructorArgument(connectionString);

            Bind<IRepository<User>>().To<UserRepository>().InSingletonScope();
            Bind<IRepository<Book>>().To<BookRepository>().InSingletonScope();
            Bind<IRepository<Statistic>>().To<StatisticRepository>().InSingletonScope();
            Bind<IRepository<Notification>>().To<NotificationRepository>().InSingletonScope();

            Bind<IUserService>().To<UserService>().InSingletonScope();
            Bind<IBookService>().To<BookService>().InSingletonScope();
            Bind<IStatisticService>().To<StatisticService>().InSingletonScope();
            Bind<INotificationService>().To<NotificationService>().InSingletonScope();
            Bind<IHashService>().To<HashService>().InSingletonScope();
        }
    }
}
