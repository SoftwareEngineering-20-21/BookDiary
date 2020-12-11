using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using BookDiary.BLL.DTO;
using BookDiary.DAL.Entities;
using BookDiary.DAL.Interfaces;
using BookDiary.BLL.Infrastructure;
using BookDiary.BLL.Interfaces;
using AutoMapper;
using System.Net.Mail;
using System.Linq;

namespace BookDiary.BLL.Services
{
    public class UserService : IUserService
    {
        private IUnitOfWork Database { get; set; }

        private IHashService HashService;

        public UserDTO CurrentUser { get; private set; }

        public UserService(IUnitOfWork uow, IHashService hashService)
        {
            CurrentUser = null;
            Database = uow;
            this.HashService = hashService;
        }

        public bool IsValidMail(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public UserDTO Login(string email, string password)
        {
            User user = Database.Users.Get().FirstOrDefault(x => x.Email == email);
            if (user == null)
            {
                throw new ArgumentException("There is no such user with this email");
            }

            UserDTO userDto = new UserDTO
            {
                Id = user.Id,
                Nickname = user.Nickname,
                Fullname = user.Fullname,
                Email = user.Email,
                Password = user.Password
            };

            if (user != null && HashService.GetHash(password) == user.Password)
            {
                CurrentUser = userDto;
            }
            else
            {
                throw new ArgumentException("The email or password is incorrect.");
            }

            return CurrentUser;
        }

        public UserDTO SignUp(string nickName, string fullName, string email, string password)
        {
            var existUser = Database.Users.Get().FirstOrDefault(x => x.Email == email);

            if (existUser == null && IsValidMail(email))
            {
                User user = new User
                {
                    Nickname = nickName,
                    Fullname = fullName,
                    Email = email,
                    Password = HashService.GetHash(password),
                };
                Database.Users.Update(user);
                Database.Save();
                CurrentUser = new UserDTO
                {
                    Id = user.Id,
                    Nickname = user.Nickname,
                    Fullname = user.Fullname,
                    Email = user.Email,
                    Password = user.Password
                };
            }
            else
            {
                throw new ArgumentException("Phone or mail incorrect");
            }

            return CurrentUser;
        }

        public UserDTO GetUser(int? Id)
        {
            if (Id == null)
            {
                throw new ValidationException("User id not set", "");
            }

            var user = Database.Users.Get(Id.Value);

            if (user == null)
            {
                throw new ValidationException("User not found", "");
            }
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
                User user = new User
                {
                    Nickname = userDto.Nickname,
                    Fullname = userDto.Fullname,
                    Email = userDto.Email,
                    Password = HashService.GetHash(userDto.Password)
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
