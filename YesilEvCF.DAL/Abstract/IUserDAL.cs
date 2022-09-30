using System.Collections.Generic;
using YesilEvCF.Core.Entities;
using YesilEvCF.Core.Interfaces;
using YesilEvCF.DTOs;

namespace YesilEvCF.DAL.Abstract
{
    public interface IUserDAL : IRepo<User>
    {
        User Login(LoginDTO loginDTO);
        void Register(RegisterDTO registerDTO);
        void AdminRegister(AdminUserAddDTO adminuseraddDTO);
        List<User> GetAllUsers();
        User GetUserByID(int id);
        User GetUserByName(string username);
        User GetUserByUserInfo(string username, string password, string email);
        void UpdateUser(User user);
        void DeleteUser(int id);
    }
}
