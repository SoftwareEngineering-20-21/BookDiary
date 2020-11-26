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
    public class NotificationService : INotificationService
    {
        IUnitOfWork Database { get; set; }

        public NotificationService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public void CreateNotification(NotificationDTO notificationDto)
        {
            Notification notification = new Notification
            {
                Day = notificationDto.Day,
                Message = notificationDto.Message,
                BookId = notificationDto.BookId
            };

            Database.Notifications.Create(notification);
            Database.Save();
        }

        public void UpdateNotification(NotificationDTO notificationDto)
        {
            Notification notification = Database.Notifications.Get(notificationDto.Id);

            if (notification == null)
            {
                throw new ValidationException("Notification not found", "");
            }
            else
            {
                notification.Day = notificationDto.Day;
                notification.Message = notificationDto.Message;
                notification.IsSeen = notificationDto.IsSeen;
                Database.Notifications.Update(notification);
            }

            Database.Save();
        }

        public void DeleteNotification(NotificationDTO notificationDto)
        {
            Notification notification = Database.Notifications.Get(notificationDto.Id);

            if (notification == null)
            {
                throw new ValidationException("Notification not found", "");
            }
            else
            {
                Database.Notifications.Delete(notification.Id);
            }

            Database.Save();
        }

        public NotificationDTO GetNotification(int? Id)
        {
            if (Id == null)
            {
                throw new ValidationException("Notification id not set", "");
            }
            var notification = Database.Notifications.Get(Id.Value);
            if (notification == null)
            {
                throw new ValidationException("Notification not found", "");
            }
            return new NotificationDTO { Day = notification.Day, Message = notification.Message, IsSeen = notification.IsSeen, BookId = notification.BookId };
        }

        public IEnumerable<NotificationDTO> GetNotifications()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Notification, NotificationDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Notification>, List<NotificationDTO>>(Database.Notifications.GetAll());
        }
    }
}
