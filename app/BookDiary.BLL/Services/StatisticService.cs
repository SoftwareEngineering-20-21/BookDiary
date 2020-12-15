﻿using System;
using BookDiary.BLL.DTO;
using BookDiary.DAL.Entities;
using BookDiary.DAL.Interfaces;
using BookDiary.BLL.Infrastructure;
using BookDiary.BLL.Interfaces;
using System.Collections.Generic;
using AutoMapper;
using System.Linq;

namespace BookDiary.BLL.Services
{
    public class StatisticService : IStatisticService
    {
        IUnitOfWork Database { get; set; }
        StatisticDTO curStat;

        public StatisticService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void CreateStatistic(StatisticDTO statisticDto)
        {
            var stat = Database.Statistics.Get().FirstOrDefault(x => x.Day.Date == statisticDto.Day.Date && x.BookId==statisticDto.BookId);
            var id = 0;
            if (stat==null)
            {
                Statistic statistic = new Statistic
                {
                    Day = statisticDto.Day.Date,
                    OldPages = statisticDto.OldPages,
                    NewPages = statisticDto.NewPages,
                    BookId = statisticDto.BookId
                };
                Database.Statistics.Create(statistic);
                Database.Save();
                List<StatisticDTO> statistics = GetStatistics().Where(
                    x => x.BookId == statistic.BookId && x.Day.Date == DateTimeOffset.Now.Date).ToList();
                
                curStat = statistics.ElementAt<StatisticDTO>(0);
                id = curStat.Id;
            }
            else
            {
                UpdateStatistic(curStat);
                Database.Save();
            }

            
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
                statistic.Day = statisticDto.Day.Date;
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
