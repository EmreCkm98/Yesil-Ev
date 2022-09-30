using System.Collections.Generic;
using System.Linq;
using YesilEvCF.Core.Context;
using YesilEvCF.Core.Entities;
using YesilEvCF.Core.Repos;
using YesilEvCF.DAL.Abstract;

namespace YesilEvCF.DAL.Concrete
{
    public class RollDAL : EFRepoBase<YesilEvDbContext, Rol>, IRollDAL
    {
        public List<Rol> GetAllRoles()
        {
            ProDAL<Rol> rolDAL = new ProDAL<Rol>();
            var rols = rolDAL.GetAll();
            return rols;
        }

        public Rol GetRoleByName(string name)
        {
            ProDAL<Rol> rolDAL = new ProDAL<Rol>();
            var rol = rolDAL.GetByFilter(x => x.RollName == name).FirstOrDefault();
            return rol;
        }
    }
}
