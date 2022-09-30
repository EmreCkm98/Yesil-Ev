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
    public class ContentDAL : EFRepoBase<YesilEvDbContext, Content>, IContentDAL
    {
        public List<ContentListDTO> GetAllContents()
        {
            ProDAL<Content> proDAL = new ProDAL<Content>();
            List<ContentListDTO> contentListDto = new List<ContentListDTO>();

            var contentList = proDAL.GetAll();
            foreach (var item in contentList)
            {
                contentListDto.Add(MappingConfig.Mapper.Map<ContentListDTO>(item));
            }
            if (contentList.Count > 0)
            {
                return contentListDto;
            }
            else
            {
                return null;
            }
        }

        public List<ContentListDTO> GetAllUserProductContents(int id)
        {
            ProDAL<Content> proDAL = new ProDAL<Content>();
            ProDAL<User> userDAL = new ProDAL<User>();
            ProDAL<Product> productDAL = new ProDAL<Product>();
            ProDAL<ProductIngredient> productIngredientDAL = new ProDAL<ProductIngredient>();

            var contentList = userDAL.GetAll().Join(productDAL.GetEntity(), x => x.UserID, y => y.UserID, (u, p) => new
            {
                user = u,
                product = p
            }).Where(x => x.user.UserID == id).Join(productIngredientDAL.GetEntity(), x => x.product.ProductID, y => y.ProductID, (p, pi) => new
            {
                user = p.user,
                product = p.product,
                productIngredient = pi
            }).Join(proDAL.GetEntity(), x => x.productIngredient.ContentID, y => y.ContentID, (pi, c) => new
            {
                user = pi.user,
                product = pi.product,
                productIngredient = pi.productIngredient,
                content = c
            }).Select(x => new ContentListDTO
            {
                ContentName = x.content.ContentName
            }).ToList();
            if (contentList.Count > 0)
            {
                return contentList;
            }
            else
            {
                return null;
            }


        }

        public Content GetContentByFilter(string name)
        {
            ProDAL<Content> proDAL = new ProDAL<Content>();
            var content = proDAL.GetByFilter(x => x.ContentName == name).FirstOrDefault();
            return content;
        }

        public Content GetContentByID(object id)
        {
            ProDAL<Content> proDAL = new ProDAL<Content>();
            var content = proDAL.GetById(id);
            return content;
        }
    }
}
