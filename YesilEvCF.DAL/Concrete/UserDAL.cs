using System.Collections.Generic;
using System.Linq;
using YesilEvCF.Core.Context;
using YesilEvCF.Core.Entities;
using YesilEvCF.Core.Repos;
using YesilEvCF.DAL.Abstract;
using YesilEvCF.DTOs;
using YesilEvCF.Mapping;

namespace YesilEvCF.DAL.Concrete
{
    public class UserDAL : EFRepoBase<YesilEvDbContext, User>, IUserDAL
    {
        public void AdminRegister(AdminUserAddDTO adminuseraddDTO)
        {
            ProDAL<User> proDAL = new ProDAL<User>();

            var userobj = MappingConfig.Mapper.Map<User>(adminuseraddDTO);
            var user = proDAL.Add(userobj);
            proDAL.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            ProDAL<User> proDAL = new ProDAL<User>();
            var user = proDAL.GetByFilter(x => x.UserID == id).SingleOrDefault();
            proDAL.Delete(user);
            proDAL.SaveChanges();
        }

        public List<User> GetAllUsers()
        {
            ProDAL<User> proDAL = new ProDAL<User>();
            var users = proDAL.GetAll();
            return users;
        }

        public User GetUserByID(int id)
        {
            ProDAL<User> proDAL = new ProDAL<User>();
            var user = proDAL.GetByFilter(x => x.UserID == id).SingleOrDefault();
            if (user != null)
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        public User GetUserByName(string username)
        {
            ProDAL<User> proDAL = new ProDAL<User>();
            var user = proDAL.GetByFilter(x => x.UserName == username).FirstOrDefault();
            if (user != null)
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        public User GetUserByUserInfo(string username, string password, string email)
        {
            ProDAL<User> proDAL = new ProDAL<User>();
            var user = proDAL.GetByFilter(x => x.UserName == username && x.Password == password && x.Email == email).FirstOrDefault();
            if (user != null)
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        public User Login(LoginDTO loginDTO)
        {
            ProDAL<User> proDAL = new ProDAL<User>();
            var user = proDAL.GetByFilter(x => x.UserName == loginDTO.UserName && x.Password == loginDTO.Password).FirstOrDefault();
            if (user != null)
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        public void Register(RegisterDTO registerDTO)
        {

            ProDAL<User> proDAL = new ProDAL<User>();

            var userobj = MappingConfig.Mapper.Map<User>(registerDTO);
            var user = proDAL.Add(userobj);
            proDAL.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            ProDAL<User> proDAL = new ProDAL<User>();
            if (user != null)
            {
                proDAL.Update(user);
                proDAL.SaveChanges();
            }
        }
    }
}
