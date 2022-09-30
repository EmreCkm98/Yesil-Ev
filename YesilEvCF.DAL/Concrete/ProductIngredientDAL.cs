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
    public class ProductIngredientDAL : EFRepoBase<YesilEvDbContext, ProductIngredient>, IProductIngredientDAL
    {
        public ProductIngredient AddProductIngredient(ProductIngredientAddDTO productIngredientAddDTO)
        {
            ProDAL<ProductIngredient> proDAL = new ProDAL<ProductIngredient>();
            if (productIngredientAddDTO != null)
            {
                var productIngredient = MappingConfig.Mapper.Map<ProductIngredient>(productIngredientAddDTO);
                proDAL.Add(productIngredient);
                proDAL.SaveChanges();
                return productIngredient;
            }
            else
            {
                return null;
            }
        }

        public void DeleteProductIngredient(ProductIngredient productIngredient)
        {
            ProDAL<ProductIngredient> proDAL = new ProDAL<ProductIngredient>();
            var pi = proDAL.GetByFilter(x => x.ProductIngredientID == productIngredient.ProductIngredientID).SingleOrDefault();
            proDAL.Delete(pi);
            proDAL.SaveChanges();
        }

        public List<ProductIngredientDTO> GetAllProductIngredients()
        {
            ProDAL<ProductIngredient> proDAL = new ProDAL<ProductIngredient>();
            ProDAL<Product> productDAL = new ProDAL<Product>();
            ProDAL<Ingredient> ingredientDAL = new ProDAL<Ingredient>();
            ProDAL<Content> contentDAL = new ProDAL<Content>();
            var ingredients = proDAL.GetAll().Join(productDAL.GetEntity(), x => x.ProductID, y => y.ProductID, (pi, p) => new
            {
                productingredient = pi,
                product = p
            }).Join(ingredientDAL.GetEntity(), x => x.productingredient.IngredientID, y => y.IngredientID, (pi, i) => new
            {
                productingredient = pi.productingredient,
                product = pi.product,
                ingredient = i
            }).Join(contentDAL.GetEntity(), x => x.productingredient.ContentID, y => y.ContentID, (pi, c) => new
            {
                productingredient = pi.productingredient,
                product = pi.product,
                ingredient = pi.ingredient,
                content = c
            }).Select(x => new ProductIngredientDTO
            {
                ProductName = x.product.ProductName,
                IngredientName = x.ingredient.IngredientName,
                IngredientDetail = x.ingredient.IngredientContent,
                ContentName = x.content.ContentName
            }).ToList();
            return ingredients;
        }

        public List<ProductBlackListDTO> GetAllUserProductIngredients(int id)
        {
            ProDAL<ProductIngredient> proDAL = new ProDAL<ProductIngredient>();
            ProDAL<Product> productDAL = new ProDAL<Product>();
            ProDAL<User> userDAL = new ProDAL<User>();
            ProDAL<Ingredient> ingredientDAL = new ProDAL<Ingredient>();
            ProDAL<Content> contentDAL = new ProDAL<Content>();
            var userProductIngredient = productDAL.GetAll().Join(userDAL.GetEntity(), x => x.UserID, y => y.UserID, (p, u) => new
            {
                productID = p.ProductID,
                productname = p.ProductName,
                userID = u.UserID,
                username = u.UserName
            }).Join(proDAL.GetEntity(), x => x.productID, y => y.ProductID, (pro, proIng) => new
            {
                productid = pro.productID,
                productname = pro.productname,
                userid = pro.userID,
                username = pro.username,
                proing = proIng
            }).Join(ingredientDAL.GetEntity(), x => x.proing.IngredientID, y => y.IngredientID, (pi, i) => new
            {
                productid = pi.productid,
                productname = pi.productname,
                userid = pi.userid,
                username = pi.username,
                proing = pi.proing,
                ingredient = i
            }).Join(contentDAL.GetEntity(), x => x.proing.ContentID, y => y.ContentID, (i, c) => new ProductBlackListDTO
            {
                ProductName = i.productname,
                UserID = i.userid,
                UserName = i.username,
                IngredientID = i.ingredient.IngredientID,
                IngredientName = i.ingredient.IngredientName,
                ContentName = c.ContentName
            }).Where(x => x.UserID == id).ToList();
            return userProductIngredient;
        }

        public List<IngredientDTO> GetAllUserProductIngredients(int userid, int productid)
        {
            ProDAL<ProductIngredient> proDAL = new ProDAL<ProductIngredient>();
            ProDAL<Product> productDAL = new ProDAL<Product>();
            ProDAL<User> userDAL = new ProDAL<User>();
            ProDAL<Ingredient> ingredientDAL = new ProDAL<Ingredient>();
            ProDAL<Content> contentDAL = new ProDAL<Content>();
            var userProduct = productDAL.GetAll().Join(userDAL.GetEntity(), x => x.UserID, y => y.UserID, (p, u) => new
            {
                productID = p.ProductID,
                productname = p.ProductName,
                userID = u.UserID,
                username = u.UserName
            }).Where(x => x.userID == userid && x.productID == productid).Join(proDAL.GetEntity(), x => x.productID, y => y.ProductID, (pro, proIng) => new
            {
                productid = pro.productID,
                productname = pro.productname,
                userid = pro.userID,
                username = pro.username,
                proing = proIng
            }).Join(ingredientDAL.GetEntity(), x => x.proing.IngredientID, y => y.IngredientID, (pi, i) => new
            {
                productid = pi.productid,
                productname = pi.productname,
                userid = pi.userid,
                username = pi.username,
                proing = pi.proing,
                ingredientname = i.IngredientName,
                ingredientid = i.IngredientID
            }).Join(contentDAL.GetEntity(), x => x.proing.ContentID, y => y.ContentID, (i, c) => new IngredientDTO
            {
                ProductID = i.productid,
                ProductName = i.productname,
                UserID = i.userid,
                IngredientID = i.ingredientid,
                IngredientName = i.ingredientname,
                ContentID = i.proing.ContentID,
                ContentName = c.ContentName
            }).ToList();
            return userProduct;
        }

        public List<ProductBlackListDTO> GetAllUserProductIngredientsByContent(int id, string name)
        {
            ProDAL<ProductIngredient> proDAL = new ProDAL<ProductIngredient>();
            ProDAL<Product> productDAL = new ProDAL<Product>();
            ProDAL<User> userDAL = new ProDAL<User>();
            ProDAL<Ingredient> ingredientDAL = new ProDAL<Ingredient>();
            ProDAL<Content> contentDAL = new ProDAL<Content>();
            var userProductIngredient = productDAL.GetAll().Join(userDAL.GetEntity(), x => x.UserID, y => y.UserID, (p, u) => new
            {
                productID = p.ProductID,
                productname = p.ProductName,
                userID = u.UserID,
                username = u.UserName
            }).Where(x => x.userID == id).Join(proDAL.GetEntity(), x => x.productID, y => y.ProductID, (pro, proIng) => new
            {
                productid = pro.productID,
                productname = pro.productname,
                userid = pro.userID,
                username = pro.username,
                proing = proIng
            }).Join(ingredientDAL.GetEntity(), x => x.proing.IngredientID, y => y.IngredientID, (pi, i) => new
            {
                productid = pi.productid,
                productname = pi.productname,
                userid = pi.userid,
                username = pi.username,
                proing = pi.proing,
                ingredientname = i.IngredientName
            }).Join(contentDAL.GetEntity(), x => x.proing.ContentID, y => y.ContentID, (i, c) => new ProductBlackListDTO
            {
                ProductName = i.productname,
                UserID = i.userid,
                UserName = i.username,
                IngredientName = i.ingredientname,
                ContentName = c.ContentName
            }).Where(x => x.ContentName != name).ToList();
            return userProductIngredient;
        }

        public List<ContentCountDTO> GetAllUserProductIngredientsContentCount(int userid, int productid)
        {
            ProDAL<ProductIngredient> proDAL = new ProDAL<ProductIngredient>();
            ProDAL<Product> productDAL = new ProDAL<Product>();
            ProDAL<User> userDAL = new ProDAL<User>();
            ProDAL<Ingredient> ingredientDAL = new ProDAL<Ingredient>();
            ProDAL<Content> contentDAL = new ProDAL<Content>();
            var contentCount = productDAL.GetAll().Join(userDAL.GetEntity(), x => x.UserID, y => y.UserID, (p, u) => new
            {
                productid = p.ProductID,
                productname = p.ProductName,
                userid = u.UserID
            }).Where(x => x.userid == userid && x.productid == productid).Join(proDAL.GetEntity(), x => x.productid, y => y.ProductID, (p, pi) => new
            {
                productid = p.productid,
                productname = p.productname,
                userid = p.userid,
                proing = pi
            }).Join(ingredientDAL.GetEntity(), x => x.proing.IngredientID, y => y.IngredientID, (pi, i) => new
            {
                productid = pi.productid,
                productname = pi.productname,
                userid = pi.userid,
                proing = pi.proing,
                ingredientid = i.IngredientID,
                ingredientname = i.IngredientName,
            }).Join(contentDAL.GetEntity(), x => x.proing.ContentID, y => y.ContentID, (i, c) => new IngredientDTO
            {
                ProductID = i.productid,
                ProductName = i.productname,
                UserID = i.userid,
                IngredientID = i.ingredientid,
                IngredientName = i.ingredientname,
                ContentID = i.proing.ContentID,
                ContentName = c.ContentName
            }).GroupBy(x => x.ContentName).Select(x => new ContentCountDTO { ContentName = x.Key, Count = x.Count() }).ToList();
            return contentCount;
        }

        public ProductIngredient GetProductIngredientByIngredientID(int id)
        {
            ProDAL<ProductIngredient> proDAL = new ProDAL<ProductIngredient>();
            var productingredient = proDAL.GetByFilter(x => x.IngredientID == id).SingleOrDefault();
            if (productingredient != null)
            {
                return productingredient;
            }
            else
            {
                return null;
            }
        }

        public ProductIngredient GetProductIngredientByProductID(int id, int ingredientId)
        {
            ProDAL<ProductIngredient> proDAL = new ProDAL<ProductIngredient>();
            var productingredient = proDAL.GetByFilter(x => x.ProductID == id && x.IngredientID == ingredientId).SingleOrDefault();
            if (productingredient != null)
            {
                return productingredient;
            }
            else
            {
                return null;
            }
        }

        public List<ProductIngredient> GetProductIngredientByProductID(int id)
        {
            ProDAL<ProductIngredient> proDAL = new ProDAL<ProductIngredient>();
            var productingredient = proDAL.GetByFilter(x => x.ProductID == id).ToList();
            if (productingredient != null)
            {
                return productingredient;
            }
            else
            {
                return null;
            }
        }

        public void UpdateProductIngredient(ProductIngredient productIngredient)
        {
            ProDAL<ProductIngredient> proDAL = new ProDAL<ProductIngredient>();
            if (productIngredient != null)
            {
                proDAL.Update(productIngredient);
                proDAL.SaveChanges();
            }
        }
    }
}
