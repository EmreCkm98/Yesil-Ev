using YesilEvCF.Core.Context;
using YesilEvCF.Core.Repos;

namespace YesilEvCF.DAL.Concrete
{
    public class ProDAL<TEntity> : EFRepoBase<YesilEvDbContext, TEntity> where TEntity : class
    {
        public ProDAL()
        {
        }

        public ProDAL(YesilEvDbContext context) : base(context)
        {
        }
    }
}
