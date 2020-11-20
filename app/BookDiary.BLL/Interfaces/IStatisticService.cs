using System;
using System.Collections.Generic;
using BookDiary.BLL.DTO;

namespace BookDiary.BLL.Interfaces
{
    public interface IStatisticService
    {
        void CreateStatistic(StatisticDTO statisticDTO);
        void UpdateStatistic(StatisticDTO statisticDTO);
        void DeleteStatistic(StatisticDTO statisticDTO);
        StatisticDTO GetStatistic(int? id);
        IEnumerable<StatisticDTO> GetStatistics();
        IEnumerable<StatisticDTO> GetStatisticsByBookId(int? bookId);
    }
}
