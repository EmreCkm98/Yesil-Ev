using System.Collections.Generic;
using YesilEvCF.Core.Entities;
using YesilEvCF.Core.Interfaces;
using YesilEvCF.DTOs;

namespace YesilEvCF.DAL.Abstract
{
    public interface IContentDAL : IRepo<Content>
    {
        List<ContentListDTO> GetAllContents();
        List<ContentListDTO> GetAllUserProductContents(int id);
        Content GetContentByFilter(string name);
        Content GetContentByID(object id);
    }
}
