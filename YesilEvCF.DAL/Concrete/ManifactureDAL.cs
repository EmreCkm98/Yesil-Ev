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
    public class ManifactureDAL : EFRepoBase<YesilEvDbContext, Manufacturer>, IManifactureDAL
    {
        public Manufacturer AddManufacture(ManifactureAddDTO manifactureAddDTO)
        {
            ProDAL<Manufacturer> proDAL = new ProDAL<Manufacturer>();

            if (manifactureAddDTO != null)
            {
                var manifacture = MappingConfig.Mapper.Map<Manufacturer>(manifactureAddDTO);
                proDAL.Add(manifacture);
                proDAL.SaveChanges();
                return manifacture;
            }
            else
            {
                return null;
            }
        }

        public List<ManifactureAddDTO> GetAllManifactures()
        {
            ProDAL<Manufacturer> proDAL = new ProDAL<Manufacturer>();
            var manifactures = proDAL.GetAll();
            List<ManifactureAddDTO> manifacturesDto = new List<ManifactureAddDTO>();
            if (manifactures.Count > 0)
            {
                foreach (var item in manifactures)
                {
                    manifacturesDto.Add(MappingConfig.Mapper.Map<ManifactureAddDTO>(item));
                }
                return manifacturesDto;
            }
            else
            {
                return null;
            }
        }

        public Manufacturer GetManifactureByFilter(string name)
        {
            ProDAL<Manufacturer> proDAL = new ProDAL<Manufacturer>();
            var manifacture = proDAL.GetByFilter(x => x.ManufacturerName == name).FirstOrDefault();
            if (manifacture != null)
            {
                return manifacture;
            }
            else
            {
                return null;
            }
        }
    }
}
