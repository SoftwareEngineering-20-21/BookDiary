using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookDiary.BLL.Interfaces;
using BookDiary.BLL.Services;
using BookDiary.BLL.DTO;
using BookDiary.DAL.Entities;
using BookDiary.DAL.Interfaces;
using BookDiary.Tests.Mocks;
using HashService = BookDiary.Tests.Mocks.HashService;

namespace BookDiary.Tests
{
    [TestClass]
    public class UserServiceTest
    {
        private IUserService userService;

        private IUnitOfWork unitOfWork;

        private User user = new User()
        {
            Id = 1,
            Email = "user1@email.com",
            Fullname = "user1",
            Nickname = "user1",
            Password = "password"
        };

        public UserServiceTest()
        {
            unitOfWork = new UnitOfWork();
            unitOfWork.Users.Create(user);
            userService = new UserService(unitOfWork, new HashService());
        }

        [TestMethod]
        public void Test_Login()
        {
            var loggedInUser = userService.Login(user.Email, user.Password);
            Assert.AreEqual(user.Id, loggedInUser.Id);
            Assert.AreEqual(user.Email, loggedInUser.Email);
            Assert.AreEqual(user.Fullname, loggedInUser.Fullname);
            Assert.AreEqual(user.Nickname, loggedInUser.Nickname);
            Assert.AreEqual(user.Password, loggedInUser.Password);
        }

        [TestMethod]
        public void Test_GetUser()
        {
            var resUser = userService.GetUser(user.Id);
            Assert.AreEqual(user.Email, resUser.Email);
            Assert.AreEqual(user.Fullname, resUser.Fullname);
            Assert.AreEqual(user.Nickname, resUser.Nickname);
        }

        [TestMethod]
        public void Test_UpdateUser()
        {
            var userDto = new UserDTO
            {
                Id = user.Id,
                Nickname = "updated",
                Fullname = " updated",
                Password = "updated",
                Email = "updated",
            };
            userService.UpdateUser(userDto);
            var resUser = unitOfWork.Users.Get(userDto.Id);
            Assert.AreEqual(userDto.Email, resUser.Email);
            Assert.AreEqual(userDto.Fullname, resUser.Fullname);
            Assert.AreEqual(userDto.Nickname, resUser.Nickname);
        }
    }
}
