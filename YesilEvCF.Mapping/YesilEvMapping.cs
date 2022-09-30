using AutoMapper;
using YesilEvCF.Core.Entities;
using YesilEvCF.DTOs;

namespace YesilEvCF.Mapping
{
    public class YesilEvMapping : Profile
    {
        public YesilEvMapping()
        {
           
            CreateMap<SearchHistory, SearchHistoryDTO>().ReverseMap();
            CreateMap<Favorite, FavoriteDTO>().ReverseMap();
            CreateMap<UserFavorite, UserFavoriteDTO>().ReverseMap();
            CreateMap<BlackList, AddBlackListDTO>().ReverseMap();
            CreateMap<User, AdminUserAddDTO>().ReverseMap();
            CreateMap<ProductStatus, ProductStatusDTO>().ReverseMap();
        }

    }
}
