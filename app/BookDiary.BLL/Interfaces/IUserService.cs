using System;

using BookDiary.BLL.DTO;

namespace BookDiary.BLL.Interfaces
{
    public interface IUserService
    {
        void CreateUser(UserDTO userDTO);
        void UpdateUser(UserDTO userDTO);
        void DeleteUSer(UserDTO userDTO);

        

    }
}
