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
    public class BlackListDAL : EFRepoBase<YesilEvDbContext, BlackList>, IBlackListDAL
    {
        public void AddBlackList(AddBlackListDTO addBlackListDTO)
        {
            ProDAL<BlackList> proDAL = new ProDAL<BlackList>();
            if (addBlackListDTO != null)
            {
                var blackList = MappingConfig.Mapper.Map<BlackList>(addBlackListDTO);
                proDAL.Add(blackList);
                proDAL.SaveChanges();
            }

        }

        public void DeleteBlackList(BlackList blackList)
        {
            ProDAL<BlackList> proDAL = new ProDAL<BlackList>();
            var blackListDb = proDAL.GetByFilter(x => x.BlackListID == blackList.BlackListID).SingleOrDefault();
            proDAL.Delete(blackListDb);
            proDAL.SaveChanges();
        }

        public List<BlackList> GetBlackListByUserID(int userid)
        {
            ProDAL<BlackList> proDAL = new ProDAL<BlackList>();
            var blackLists = proDAL.GetByFilter(x => x.UserID == userid).ToList();
            return blackLists;
        }
    }
}
