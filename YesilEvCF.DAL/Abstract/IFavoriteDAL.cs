using System.Collections.Generic;
using YesilEvCF.Core.Entities;
using YesilEvCF.Core.Interfaces;
using YesilEvCF.DTOs;

namespace YesilEvCF.DAL.Abstract
{
    public interface IFavoriteDAL : IRepo<Favorite>
    {
        List<FavoriteDTO> GetAllFavorites();
        Favorite AddFavorite(FavoriteDTO favorite);
        void AddUserFavorite(UserFavoriteDTO favorite);
        void UpdateUserFavorite(UserFavorite userFavorite);
        Favorite GetFavoriteByName(string name);
        UserFavorite GetUserFavoriteByIDName(int userid, int productid);
        List<UserFavorite> GetUserFavoriteByProductID(int productid);
        List<UserFavorite> GetUserFavoriteByUserID(int userid);
        void DeleteUserFavorite(UserFavorite favorite);
    }
}
