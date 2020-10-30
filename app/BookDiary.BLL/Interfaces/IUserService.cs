using System;

using BookDiary.BLL.DTO;

namespace BookDiary.BLL.Interfaces
{
    public interface IUserService
    {
        UserDTO GetUser(int? Id);
        void CreateUser(UserDTO userDTO);
        void UpdateUser(UserDTO userDTO);
        void DeleteUSer(UserDTO userDTO);

        

    }
}
