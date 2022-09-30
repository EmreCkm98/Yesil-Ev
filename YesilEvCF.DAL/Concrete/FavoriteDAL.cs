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
    public class FavoriteDAL : EFRepoBase<YesilEvDbContext, Favorite>, IFavoriteDAL
    {
        public Favorite AddFavorite(FavoriteDTO favorite)
        {
            Favorite addedFavorite = null;
            ProDAL<Favorite> proDAL = new ProDAL<Favorite>();
            if (favorite != null)
            {
                var favoritedb = MappingConfig.Mapper.Map<Favorite>(favorite);
                addedFavorite = proDAL.Add(favoritedb);
                proDAL.SaveChanges();
            }
            return addedFavorite;
        }

        public void AddUserFavorite(UserFavoriteDTO favorite)
        {
            ProDAL<UserFavorite> proDAL = new ProDAL<UserFavorite>();
            if (favorite != null)
            {
                var favoritedb = MappingConfig.Mapper.Map<UserFavorite>(favorite);
                proDAL.Add(favoritedb);
                proDAL.SaveChanges();
            }

        }

        public void DeleteUserFavorite(UserFavorite favorite)
        {
            ProDAL<UserFavorite> proDAL = new ProDAL<UserFavorite>();
            var uf = proDAL.GetByFilter(x => x.UserFavoriteID == favorite.UserFavoriteID).SingleOrDefault();
            proDAL.Delete(uf);
            proDAL.SaveChanges();
        }

        public List<FavoriteDTO> GetAllFavorites()
        {

            ProDAL<Favorite> proDAL = new ProDAL<Favorite>();
            List<FavoriteDTO> favoritesDto = new List<FavoriteDTO>();
            var favorites = proDAL.GetAll();
            foreach (var item in favorites)
            {
                favoritesDto.Add(MappingConfig.Mapper.Map<FavoriteDTO>(item));
            }
            return favoritesDto;
        }

        public Favorite GetFavoriteByName(string name)
        {
            ProDAL<Favorite> proDAL = new ProDAL<Favorite>();
            var favoriteNameDb = proDAL.GetByFilter(x => x.FavoriteName == name).FirstOrDefault();
            return favoriteNameDb;
        }

        public UserFavorite GetUserFavoriteByIDName(int userid, int productid)
        {
            ProDAL<UserFavorite> proDAL = new ProDAL<UserFavorite>();
            var userfavoritedb = proDAL.GetByFilter(x => x.ProductID == productid && x.UserID == userid).FirstOrDefault();
            return userfavoritedb;
        }

        public List<UserFavorite> GetUserFavoriteByProductID(int productid)
        {
            ProDAL<UserFavorite> proDAL = new ProDAL<UserFavorite>();
            var userfavoritedb = proDAL.GetByFilter(x => x.ProductID == productid).ToList();
            return userfavoritedb;
        }

        public List<UserFavorite> GetUserFavoriteByUserID(int userid)
        {
            ProDAL<UserFavorite> proDAL = new ProDAL<UserFavorite>();
            var userfavoritedb = proDAL.GetByFilter(x => x.UserID == userid).ToList();
            return userfavoritedb;
        }

        public void UpdateUserFavorite(UserFavorite userFavorite)
        {
            ProDAL<UserFavorite> proDAL = new ProDAL<UserFavorite>();
            if (userFavorite != null)
            {
                proDAL.Update(userFavorite);
                proDAL.SaveChanges();
            }
        }
    }
}
