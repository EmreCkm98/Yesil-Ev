using System.Collections.Generic;
using YesilEvCF.Core.Entities;
using YesilEvCF.Core.Interfaces;

namespace YesilEvCF.DAL.Abstract
{
    internal interface IRollDAL : IRepo<Rol>
    {
        List<Rol> GetAllRoles();
        Rol GetRoleByName(string name);
    }
}
