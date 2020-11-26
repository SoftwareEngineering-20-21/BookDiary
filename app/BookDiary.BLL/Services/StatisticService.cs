using System;
using BookDiary.BLL.DTO;
using BookDiary.DAL.Entities;
using BookDiary.DAL.Interfaces;
using BookDiary.BLL.Infrastructure;
using BookDiary.BLL.Interfaces;
using System.Collections.Generic;
using AutoMapper;

namespace BookDiary.BLL.Services
{
    public class StatisticService : IStatisticService
    {
        IUnitOfWork Database { get; set; }

        public StatisticService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void CreateStatistic(StatisticDTO statisticDto)
        {
            Statistic statistic = new Statistic
            {
                Day = statisticDto.Day,
                OldPages = statisticDto.OldPages,
                NewPages = statisticDto.NewPages,
                BookId = statisticDto.BookId
            };

            Database.Statistics.Create(statistic);
            Database.Save();
        }

        public void UpdateStatistic(StatisticDTO statisticDto)
        {
            Statistic statistic = Database.Statistics.Get(statisticDto.Id);

            if (statistic == null)
            {
                throw new ValidationException("Statistic not found", "");
            }
            else
            {
                statistic.Day = statisticDto.Day;
                statistic.OldPages = statisticDto.OldPages;
                statistic.NewPages = statisticDto.NewPages;
                Database.Statistics.Update(statistic);
            }

            Database.Save();
        }

        public void DeleteStatistic(StatisticDTO statisticDto)
        {
            Statistic statistic = Database.Statistics.Get(statisticDto.Id);

            if (statistic == null)
            {
                throw new ValidationException("Statistic not found", "");
            }
            else
            {
                Database.Statistics.Delete(statistic.Id);
            }

            Database.Save();
        }

        public StatisticDTO GetStatistic(int? Id)
        {
            if (Id == null)
            {
                throw new ValidationException("Statistic id not set", "");
            }
            var statistic = Database.Statistics.Get(Id.Value);
            if (statistic == null)
            {
                throw new ValidationException("Statistic not found", "");
            }
            return new StatisticDTO { Day = statistic.Day, OldPages = statistic.OldPages, NewPages = statistic.NewPages, BookId = statistic.BookId};
        }

        public IEnumerable<StatisticDTO> GetStatistics()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Statistic, StatisticDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Statistic>, List<StatisticDTO>>(Database.Statistics.GetAll());
        }

        public IEnumerable<StatisticDTO> GetStatisticsByBookId(int? bookId)
        {
            if (bookId == null)
            {
                throw new ValidationException("Book id not set", "");
            }
            Book book = Database.Books.Get(bookId.Value);
            if (book == null)
            {
                throw new ValidationException("Book not found", "");
            }
            else
            {
                // TODO !!!!!!!!!!!!!!!!!!!!!!!!!!!!
                return null;
            }
        }
    }
}
