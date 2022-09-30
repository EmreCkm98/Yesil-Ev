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
    public class IngredientDAL : EFRepoBase<YesilEvDbContext, Ingredient>, IIngredientDAL
    {
        public Ingredient AddIngredient(IngredientAddDTO ingredientAddDTO)
        {
            ProDAL<Ingredient> proDAL = new ProDAL<Ingredient>();
            if (ingredientAddDTO != null)
            {
                var ingredient = MappingConfig.Mapper.Map<Ingredient>(ingredientAddDTO);
                proDAL.Add(ingredient);
                proDAL.SaveChanges();
                return ingredient;
            }
            else
            {
                return null;
            }
        }

        public List<IngredientListDTO> GetAllIngredients()
        {
            ProDAL<Ingredient> proDAL = new ProDAL<Ingredient>();
            List<IngredientListDTO> ingredientListDto = new List<IngredientListDTO>();

            var productList = proDAL.GetAll();
            foreach (var item in productList)
            {
                ingredientListDto.Add(MappingConfig.Mapper.Map<IngredientListDTO>(item));
            }
            if (productList.Count > 0)
            {
                return ingredientListDto;
            }
            else
            {
                return null;
            }
        }

        public List<IngredientListDTO> GetAllUserIngredients(int id, string name)
        {
            ProDAL<Ingredient> proDAL = new ProDAL<Ingredient>();
            ProDAL<User> userDAL = new ProDAL<User>();
            ProDAL<Product> productDAL = new ProDAL<Product>();
            ProDAL<ProductIngredient> productIngredientDAL = new ProDAL<ProductIngredient>();

            var contentList = userDAL.GetAll().Join(productDAL.GetEntity(), x => x.UserID, y => y.UserID, (u, p) => new
            {
                user = u,
                product = p
            }).Where(x => x.user.UserID == id && x.product.ProductName == name).Join(productIngredientDAL.GetEntity(), x => x.product.ProductID, y => y.ProductID, (p, pi) => new
            {
                user = p.user,
                product = p.product,
                productIngredient = pi
            }).Join(proDAL.GetEntity(), x => x.productIngredient.IngredientID, y => y.IngredientID, (pi, i) => new
            {
                user = pi.user,
                product = pi.product,
                productIngredient = pi.productIngredient,
                ingredient = i
            }).Select(x => new IngredientListDTO
            {
                IngredientName = x.ingredient.IngredientName,
                IngredientContent = x.ingredient.IngredientContent
            }).ToList();
            if (contentList.Count > 0)
            {
                return contentList;
            }
            else
            {
                return null;
            }
        }

        public IngredientListDTO GetAllUserIngredientsByIngredientName(int id, string productName, string Ingredientname)
        {
            ProDAL<Ingredient> proDAL = new ProDAL<Ingredient>();
            ProDAL<User> userDAL = new ProDAL<User>();
            ProDAL<Product> productDAL = new ProDAL<Product>();
            ProDAL<ProductIngredient> productIngredientDAL = new ProDAL<ProductIngredient>();

            var contentList = userDAL.GetAll().Join(productDAL.GetEntity(), x => x.UserID, y => y.UserID, (u, p) => new
            {
                user = u,
                product = p
            }).Where(x => x.user.UserID == id && x.product.ProductName == productName).Join(productIngredientDAL.GetEntity(), x => x.product.ProductID, y => y.ProductID, (p, pi) => new
            {
                user = p.user,
                product = p.product,
                productIngredient = pi
            }).Join(proDAL.GetEntity(), x => x.productIngredient.IngredientID, y => y.IngredientID, (pi, i) => new
            {
                user = pi.user,
                product = pi.product,
                productIngredient = pi.productIngredient,
                ingredient = i
            }).Where(x => x.ingredient.IngredientName == Ingredientname).Select(x => new IngredientListDTO
            {
                IngredientName = x.ingredient.IngredientName,
                IngredientContent = x.ingredient.IngredientContent
            }).FirstOrDefault();
            return contentList;
        }

        public Ingredient GetIngredientByContentID(int id)
        {
            ProDAL<Ingredient> proDAL = new ProDAL<Ingredient>();

            var ingredient = proDAL.GetByFilter(x => x.IngredientID == id).SingleOrDefault();//buraya bak!!!!!!!!!
            if (ingredient != null)
            {
                return ingredient;
            }
            else
            {
                return null;
            }
        }

        public Ingredient GetIngredientByFilter(string ingredientname)
        {
            ProDAL<Ingredient> proDAL = new ProDAL<Ingredient>();

            var ingredient = proDAL.GetByFilter(x => x.IngredientName == ingredientname).FirstOrDefault();
            if (ingredient != null)
            {
                return ingredient;
            }
            else
            {
                return null;
            }
        }

        public bool IngredientAnyExist(string ingredientname)
        {
            ProDAL<Ingredient> proDAL = new ProDAL<Ingredient>();
            bool ingredient = proDAL.Any(x => x.IngredientName == ingredientname);
            return ingredient;
        }

        public void UpdateIngredient(Ingredient ingredient)
        {
            ProDAL<Ingredient> proDAL = new ProDAL<Ingredient>();
            if (ingredient != null)
            {
                proDAL.Update(ingredient);
                proDAL.SaveChanges();
            }
        }
    }
}
