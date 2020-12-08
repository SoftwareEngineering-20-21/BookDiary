using System;

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
            return new UserDTO {Nickname = user.Nickname, Fullname=user.Fullname,Email=user.Email};
        }


        public void CreateUser(UserDTO userDto)
        {            
            if (Database.Users.Get(userDto.Id) != null)
            {
                throw new ValidationException("user already exists", "");
            }
            else
            {
                HashService Hash = new HashService();

                User user = new User
                {
                    Nickname = userDto.Nickname,
                    Fullname = userDto.Fullname,
                    Email = userDto.Email,
                    Password = Hash.GetHash(userDto.Password)
                };
                Database.Users.Create(user);
                Database.Save();
            }
        }

        public void UpdateUser(UserDTO userDto)
        {
            

            User user = Database.Users.Get(userDto.Id);

            if (user == null)
            {
                throw new ValidationException("User not found", "");
            }
            else
            {
                HashService Hash = new HashService();

                user.Nickname = userDto.Nickname;
                user.Fullname = userDto.Fullname;
                user.Email = userDto.Email;
                user.Password = Hash.GetHash(userDto.Password);

                Database.Users.Update(user);
                Database.Save();
            }
        }

        public void DeleteUser(UserDTO userDto)
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

        public IEnumerable<UserDTO> GetUsers()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<User>, List<UserDTO>>(Database.Users.GetAll());
        }
    }

}
