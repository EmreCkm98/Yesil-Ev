using System.Collections.Generic;
using YesilEvCF.Core.Entities;
using YesilEvCF.Core.Interfaces;
using YesilEvCF.DTOs;

namespace YesilEvCF.DAL.Abstract
{
    public interface IBlackListDAL : IRepo<BlackList>
    {
        void AddBlackList(AddBlackListDTO addBlackListDTO);
        List<BlackList> GetBlackListByUserID(int userid);
        void DeleteBlackList(BlackList blackList);
    }
}
