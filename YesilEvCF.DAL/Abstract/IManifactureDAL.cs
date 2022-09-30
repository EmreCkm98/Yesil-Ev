using System.Collections.Generic;
using YesilEvCF.Core.Entities;
using YesilEvCF.Core.Interfaces;
using YesilEvCF.DTOs;

namespace YesilEvCF.DAL.Abstract
{
    public interface IManifactureDAL : IRepo<Manufacturer>
    {
        Manufacturer AddManufacture(ManifactureAddDTO manifactureAddDTO);
        List<ManifactureAddDTO> GetAllManifactures();
        Manufacturer GetManifactureByFilter(string name);
    }
}
