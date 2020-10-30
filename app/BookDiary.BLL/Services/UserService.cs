using System;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;
using BookDiary.BLL.DTO;
using BookDiary.DAL.Entities;
using BookDiary.DAL.Interfaces;
using BookDiary.BLL.Infrastructure;
using BookDiary.BLL.Interfaces;

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
        public UserDTO GetUser(int? Id)
        {
            if (Id == null)
                throw new ValidationException("User id not set", "");
            var user = Database.Users.Get(Id.Value);
            if (user == null)
                throw new ValidationException("User not found", "");
            return new UserDTO {Nickname = user.Nickname, Fullname=user.Fullname,Email=user.Email,Password=user.Password };
        }


        public void CreateUser(UserDTO userDto)
        {
            User user = new User
            {
                Nickname = userDto.Nickname,
                Fullname = userDto.Fullname,
                Email = userDto.Email,
                Password = GetHash(userDto.Password)
            };
            Database.Users.Create(user);
            Database.Save();

        }

        public void UpdateUser(UserDTO userDto)
        {
            User user = Database.Users.Get(userDto.Id);

           if(user.Nickname != userDto.Nickname)
            user.Nickname = userDto.Nickname;

           if(user.Fullname != userDto.Fullname)
            user.Fullname = userDto.Fullname;

           if(user.Email!= userDto.Email)
            user.Email = userDto.Email;

           if(user.Password != userDto.Password)
            user.Password = GetHash(userDto.Password);

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

        public  string GetHash(string password)
        {
            
            SHA1CryptoServiceProvider sh = new SHA1CryptoServiceProvider();
            sh.ComputeHash(ASCIIEncoding.ASCII.GetBytes(password));
            byte[] re = sh.Hash;
            StringBuilder sb = new StringBuilder();
            foreach (byte b in re)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }

    }

}
