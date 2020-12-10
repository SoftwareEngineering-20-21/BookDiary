using System;
using System.Collections.Generic;
using BookDiary.BLL.DTO;

namespace BookDiary.BLL.Interfaces
{
    public interface IUserService
    {
        UserDTO CurrentUser { get; }
        UserDTO Login(string email, string password);
        UserDTO SignUp(string nickName, string fullName, string email, string password);
        bool IsValidMail(string emailaddress);
        UserDTO GetUser(int? Id);
        void CreateUser(UserDTO userDTO);
        void UpdateUser(UserDTO userDTO);
        void DeleteUser(UserDTO userDTO);
        IEnumerable<UserDTO> GetUsers();

        String GetTitle();
    }
}
