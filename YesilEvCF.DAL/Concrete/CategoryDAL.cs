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
    public class CategoryDAL : EFRepoBase<YesilEvDbContext, Category>, ICategoryDAL
    {
        public List<CategoryListDTO> GetAllCategories()
        {
            ProDAL<Category> proDAL = new ProDAL<Category>();
            var categoryList = proDAL.GetAll();
            List<CategoryListDTO> categoryListDto = new List<CategoryListDTO>();

            foreach (var item in categoryList)
            {
                categoryListDto.Add(MappingConfig.Mapper.Map<CategoryListDTO>(item));
            }
            return categoryListDto;
        }

        public Category GetCategoryByFilter(string name)
        {
            ProDAL<Category> proDAL = new ProDAL<Category>();
            var category = proDAL.GetByFilter(x => x.CategoryName == name).FirstOrDefault();
            if (category != null)
            {
                return category;
            }
            else
            {
                return null;
            }
        }
    }
}
