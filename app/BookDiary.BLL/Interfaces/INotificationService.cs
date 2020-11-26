using System.Collections.Generic;
using BookDiary.BLL.DTO;

namespace BookDiary.BLL.Interfaces
{
    public interface INotificationService
    {
        void CreateNotification(NotificationDTO notificationDTO);
        void UpdateNotification(NotificationDTO notificationDTO);
        void DeleteNotification(NotificationDTO notificationDTO);
        NotificationDTO GetNotification(int? id);
        IEnumerable<NotificationDTO> GetNotifications();
    }
}