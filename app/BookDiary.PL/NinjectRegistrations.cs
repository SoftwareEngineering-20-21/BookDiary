using System.Net.NetworkInformation;
using BookDiary.BLL.Interfaces;
using BookDiary.BLL.Services;
using BookDiary.DAL.EF;
using BookDiary.DAL.Entities;
using BookDiary.DAL.Interfaces;
using BookDiary.DAL.Repositories;
using Ninject.Modules;

namespace BookDiary.PL
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<AppDbContext>().ToSelf().InSingletonScope();
            Bind<IRepository<User>>().To<UserRepository>().InSingletonScope();
            Bind<IRepository<Book>>().To<BookRepository>().InSingletonScope();
            Bind<IRepository<Statistic>>().To<StatisticRepository>().InSingletonScope();
            Bind<IRepository<Notification>>().To<NotificationRepository>().InSingletonScope();

            Bind<IUnitOfWork>().To<EFUnitOfWork>().InSingletonScope();

            Bind<IUserService>().To<UserService>().InSingletonScope();
            Bind<IBookService>().To<BookService>().InSingletonScope();
            Bind<IStatisticService>().To<StatisticService>().InSingletonScope();
            Bind<INotificationService>().To<NotificationService>().InSingletonScope();
            Bind<IHashService>().To<HashService>().InSingletonScope();
        }
    }
}
