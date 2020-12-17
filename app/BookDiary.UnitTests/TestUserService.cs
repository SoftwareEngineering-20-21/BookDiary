using BookDiary.BLL.DTO;
using BookDiary.BLL.Infrastructure;
using BookDiary.BLL.Interfaces;
using BookDiary.BLL.Services;
using BookDiary.DAL.Entities;
using BookDiary.DAL.Interfaces;
using BookDiary.DAL.Repositories;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace BookDiary.UnitTests
{
    public class TestUserService
    {
        IHashService hashService = new HashService();
        private string password = "test";
        private string passwordHash;

        [Test]
        public void TestLogin()
        {
            passwordHash = hashService.GetHash(password);
            User testUser = new User
            {
                Nickname = "test",
                Fullname = "Test Testovych",
                Email = "test@test.com",
                Password = passwordHash
            };
            var mock = new Mock<IRepository<User>>();
            mock.Setup(a => a.Get()).Returns(new List<User> { testUser });
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUnitOfWork.Setup(uow => uow.Users).Returns(mock.Object);
            var service = new UserService(mockUnitOfWork.Object, hashService);
            var exc = Assert.Throws<ArgumentException>(() => service.Login("test@test.com", "teest"));
            Assert.AreEqual(exc.Message, "The email or password is incorrect.");
            UserDTO testLogin = service.Login("test@test.com", password);
            Assert.AreEqual(testLogin.Email, "test@test.com");
            Assert.AreEqual(testLogin.Password, passwordHash);
        }

        [Test]
        public void TestSignUp()
        {
            passwordHash = hashService.GetHash(password);
            User testUser = new User
            {
                Nickname = "test",
                Fullname = "Test Testovych",
                Email = "test@test.com",
                Password = passwordHash
            };
            var mock = new Mock<IRepository<User>>();
            mock.Setup(a => a.Get()).Returns(new List<User> { testUser } );
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(uow => uow.Users).Returns(mock.Object);
            var service = new UserService(mockUnitOfWork.Object, hashService);

            var exc = Assert.Throws<ArgumentException>(() => service.SignUp("test2", "Test Testovych2", "test@test.com", password));
            Assert.AreEqual(exc.Message, "Phone or mail incorrect");
            UserDTO userDto = service.SignUp("test2", "Test Testovych2", "test2@test.com", password);
            Assert.IsNotNull(userDto);
            Assert.AreEqual(userDto.Email, "test2@test.com");
            Assert.IsTrue(userDto.Password == hashService.GetHash(password));
        }
    }
}