using System;
using BookDiary.BLL.DTO;
using BookDiary.DAL.Entities;
using BookDiary.DAL.Interfaces;
using BookDiary.BLL.Infrastructure;
using BookDiary.BLL.Interfaces ;
using System.Collections.Generic;
using AutoMapper;

namespace BookDiary.BLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public void CreateUser(UserDTO userDto)
        {
            User user = new User
            {
                Nickname = userDto.Nickname,
                Fullname = userDto.Fullname,
                Email = userDto.Email,
                Password = userDto.Password
            };
            Database.Users.Create(user);
            Database.Save();

        }
        public void UpdateUser(UserDTO userDto)
        {
            User user = Database.Users.Get(userDto.Id);

            user.Nickname = userDto.Nickname;
            user.Fullname = userDto.Fullname;
            user.Email = userDto.Email;
            user.Password = userDto.Password;

            Database.Users.Update(user);
            Database.Save();
        }

        public void DeleteUSer(UserDTO userDto)
        {

            User user = Database.Users.Get(userDto.Id);

            if (user == null)
            {
                throw new ValidationException("User not found","");
            }
            else
            {
                Database.Users.Delete(user.Id);
            }

            Database.Save();
        }


    }

}
