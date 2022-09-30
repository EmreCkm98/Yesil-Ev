using System.Collections.Generic;
using YesilEvCF.Core.Entities;
using YesilEvCF.Core.Interfaces;
using YesilEvCF.DTOs;

namespace YesilEvCF.DAL.Abstract
{
    public interface ICategoryDAL : IRepo<Category>
    {
        List<CategoryListDTO> GetAllCategories();
        Category GetCategoryByFilter(string name);
    }
}
