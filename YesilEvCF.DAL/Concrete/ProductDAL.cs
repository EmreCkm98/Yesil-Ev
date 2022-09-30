using System;
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
    public class ProductDAL : EFRepoBase<YesilEvDbContext, Product>, IProductDAL
    {
        public Product AddProduct(ProductAddDTO productAddDTO)
        {
            ProDAL<Product> proDAL = new ProDAL<Product>();
            if (productAddDTO != null)
            {
                var product = MappingConfig.Mapper.Map<Product>(productAddDTO);
                proDAL.Add(product);
                proDAL.SaveChanges();
                return product;
            }
            else
            {
                return null;
            }
        }

        public bool AddSearchHistory(SearchHistory searchHistory)
        {
            ProDAL<SearchHistory> proDAL = new ProDAL<SearchHistory>();
            if (searchHistory != null)
            {
                proDAL.Add(searchHistory);
                proDAL.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public ProductListDTO BarcodeSearch(Guid guid)
        {
            ProDAL<Product> proDAL = new ProDAL<Product>();
            var product = proDAL.GetByFilter(x => x.Barcode == guid).FirstOrDefault();


            var userobj = MappingConfig.Mapper.Map<ProductListDTO>(product);
            if (product != null)
            {
                return userobj;
            }
            else
            {
                return null;
            }
        }

        public Product DeleteProduct(int id)
        {
            ProDAL<Product> proDAL = new ProDAL<Product>();
            var product = proDAL.GetByFilter(x => x.ProductID == id).SingleOrDefault();
            var deletedProduct = proDAL.Delete(product);
            proDAL.SaveChanges();
            return deletedProduct;
        }

        public List<Product> GetAllProduct()
        {
            ProDAL<Product> proDAL = new ProDAL<Product>();
            var product = proDAL.GetAll();

            if (product != null)
            {
                return product;
            }
            else
            {
                return null;
            }
        }

        public List<UserProductCategoryIngredientContentDTO> GetAllProductByCategoryByIngredient(int userid, string ingredient)
        {
            ProDAL<Product> proDAL = new ProDAL<Product>();
            ProDAL<User> userDAL = new ProDAL<User>();
            ProDAL<Manufacturer> manufactureDAL = new ProDAL<Manufacturer>();
            ProDAL<Category> categoryDAL = new ProDAL<Category>();
            ProDAL<ProductIngredient> productingredientDAL = new ProDAL<ProductIngredient>();
            ProDAL<Ingredient> ingredientDAL = new ProDAL<Ingredient>();
            ProDAL<Content> contentDAL = new ProDAL<Content>();
            var userProduct = proDAL.GetAll().Join(userDAL.GetEntity(), x => x.UserID, y => y.UserID, (p, u) => new
            {
                productid = p.ProductID,
                manufactureid = p.ManufacturerID,
                categoryid = p.CategoryID,
                productname = p.ProductName,
                userid = u.UserID
            }).Where(x => x.userid == userid).Join(manufactureDAL.GetEntity(), x => x.manufactureid, y => y.ManufacturerID, (p, m) => new
            {
                productid = p.productid,
                manufactureid = p.manufactureid,
                categoryid = p.categoryid,
                productname = p.productname,
                userid = p.userid,
                manufacturename = m.ManufacturerName
            }).Join(categoryDAL.GetEntity(), x => x.categoryid, y => y.CategoryID, (p, c) => new
            {
                productid = p.productid,
                manufactureid = p.manufactureid,
                categoryid = p.categoryid,
                productname = p.productname,
                userid = p.userid,
                manufacturename = p.manufacturename,
                categoryname = c.CategoryName
            }).Join(productingredientDAL.GetEntity(), x => x.productid, y => y.ProductID, (p, pi) => new
            {
                productid = p.productid,
                productname = p.productname,
                userid = p.userid,
                manufactureid = p.manufactureid,
                categoryid = p.categoryid,
                manufacturename = p.manufacturename,
                categoryname = p.categoryname,
                proing = pi
            }).Join(ingredientDAL.GetEntity(), x => x.proing.IngredientID, y => y.IngredientID, (pi, i) => new
            {
                productid = pi.productid,
                productname = pi.productname,
                userid = pi.userid,
                manufactureid = pi.manufactureid,
                categoryid = pi.categoryid,
                manufacturename = pi.manufacturename,
                categoryname = pi.categoryname,
                proing = pi.proing,
                ingredientid = i.IngredientID,
                ingredientname = i.IngredientName,
                ingredientdetail = i.IngredientContent,
            }).Where(x => x.ingredientname == ingredient).Join(contentDAL.GetEntity(), x => x.proing.ContentID, y => y.ContentID, (i, c) => new UserProductCategoryIngredientContentDTO
            {
                ProductID = i.productid,
                ProductName = i.productname,
                UserID = i.userid,
                ManufacturerID = i.manufactureid,
                CategoryID = i.categoryid,
                ManufacturerName = i.manufacturename,
                CategoryName = i.categoryname,
                IngredientID = i.ingredientid,
                IngredientName = i.ingredientname,
                IngredientContent = i.ingredientdetail,
                ContentID = i.proing.ContentID,
                ContentName = c.ContentName
            }).ToList();
            return userProduct;
        }

        public List<Product> GetAllProductByUserID(int id)
        {
            ProDAL<Product> proDAL = new ProDAL<Product>();
            var products = proDAL.GetByFilter(x => x.UserID == id).ToList();
            return products;
        }

        public List<ProductFavoriteDTO> GetAllUserFavoriteProduct(int id)
        {
            ProDAL<Product> proDAL = new ProDAL<Product>();
            ProDAL<UserFavorite> userFavoriteproDAL = new ProDAL<UserFavorite>();
            ProDAL<User> userproDAL = new ProDAL<User>();
            ProDAL<Favorite> favoriteproDAL = new ProDAL<Favorite>();
            var productList = userFavoriteproDAL.GetAll().Join(userproDAL.GetEntity(), x => x.UserID, y => y.UserID, (uf, u) => new
            {
                userFavorite = uf,
                user = u
            }).Where(x => x.user.UserID == id).Join(favoriteproDAL.GetEntity(), x => x.userFavorite.FavoriteID, y => y.FavoriteID, (uf, f) => new
            {
                userFavorite = uf.userFavorite,
                user = uf.user,
                favorite = f
            }).Join(proDAL.GetEntity(), x => x.userFavorite.ProductID, y => y.ProductID, (uf, p) => new
            {
                userFavorite = uf.userFavorite,
                user = uf.user,
                favorite = uf.favorite,
                product = p
            }).Select(x => new ProductFavoriteDTO
            {
                ProductName = x.product.ProductName,
                CreatedDate = x.userFavorite.CreatedDate.ToString(),
                FavoriteName = x.favorite.FavoriteName,
                UserName = x.user.UserName
            }).ToList();
            return productList;
        }

        public List<ProductListDTO> GetAllUserProduct(int id)
        {
            ProDAL<Product> proDAL = new ProDAL<Product>();
            ProDAL<User> userproDAL = new ProDAL<User>();
            var productList = proDAL.GetAll().Join(userproDAL.GetEntity(), x => x.UserID, y => y.UserID, (p, u) => new
            {
                product = p,
                user = u
            }).Where(x => x.user.UserID == id).Select(x => new ProductListDTO
            {
                ProductName = x.product.ProductName,
                Barcode = x.product.Barcode,
                ProductBackImage = x.product.ProductBackImage,
                ProductFrontImage = x.product.ProductFrontImage,
                ProductID = x.product.ProductID
            }).ToList();
            if (productList != null)
            {
                return productList;
            }
            else
            {
                return null;
            }
        }

        public List<ProductDetailDTO> GetAllUserProductCategory(int userid, int productid)
        {
            ProDAL<Product> proDAL = new ProDAL<Product>();
            ProDAL<User> userproDAL = new ProDAL<User>();
            ProDAL<Manufacturer> manifactureDAL = new ProDAL<Manufacturer>();
            ProDAL<Category> categoryDAL = new ProDAL<Category>();
            var userProduct = proDAL.GetAll().Join(userproDAL.GetEntity(), x => x.UserID, y => y.UserID, (p, u) => new
            {
                productid = p.ProductID,
                productname = p.ProductName,
                productmanufacture = p.ManufacturerID,
                productcategory = p.CategoryID,
                userid = u.UserID
            }).Where(x => x.userid == userid && x.productid == productid).Join(manifactureDAL.GetEntity(), x => x.productmanufacture, y => y.ManufacturerID, (p, mf) => new
            {
                productid = p.productid,
                productname = p.productname,
                productmanufactureid = p.productmanufacture,
                productcategoryid = p.productcategory,
                userid = p.userid,
                manufacturename = mf.ManufacturerName
            }).Join(categoryDAL.GetEntity(), x => x.productcategoryid, y => y.CategoryID, (p, c) => new ProductDetailDTO
            {
                ProductID = p.productid,
                ProductName = p.productname,
                ManufactureID = p.productmanufactureid,
                CategoryID = p.productcategoryid,
                UserID = p.userid,
                ManufactureName = p.manufacturename,
                CategoryName = c.CategoryName
            }).ToList();
            return userProduct;
        }

        public List<ProductIngredientDTO> GetAllUserProductDetail(int id)
        {
            ProDAL<Product> proDAL = new ProDAL<Product>();
            ProDAL<User> userproDAL = new ProDAL<User>();
            ProDAL<Ingredient> ingredientproDAL = new ProDAL<Ingredient>();
            ProDAL<ProductIngredient> productingredientDAL = new ProDAL<ProductIngredient>();
            ProDAL<Content> contentproDAL = new ProDAL<Content>();

            var userProduct = proDAL.GetAll().Join(userproDAL.GetEntity(), x => x.UserID, y => y.UserID, (p, u) => new
            {
                product = p,
                user = u
            }).Join(productingredientDAL.GetEntity(), x => x.product.ProductID, y => y.ProductID, (p, pi) => new
            {
                product = p.product,
                user = p.user,
                productingredient = pi
            }).Join(ingredientproDAL.GetEntity(), x => x.productingredient.IngredientID, y => y.IngredientID, (pi, i) => new
            {
                product = pi.product,
                user = pi.user,
                productingredient = pi.productingredient,
                ingredient = i
            }).Join(contentproDAL.GetEntity(), x => x.productingredient.ContentID, y => y.ContentID, (i, c) => new
            {
                product = i.product,
                user = i.user,
                productingredient = i.productingredient,
                ingredient = i.ingredient,
                content = c
            }).Where(x => x.user.UserID == id).
              Select(y => new ProductIngredientDTO
              {
                  ProductName = y.product.ProductName,
                  IngredientName = y.ingredient.IngredientName,
                  IngredientDetail = y.ingredient.IngredientContent,
                  ContentName = y.content.ContentName
              }).ToList();
            return userProduct;
        }

        public Product GetProductByBarcode(Guid id)
        {
            ProDAL<Product> proDAL = new ProDAL<Product>();
            var product = proDAL.GetByFilter(x => x.Barcode == id).FirstOrDefault();
            if (product != null)
            {
                return product;
            }
            else
            {
                return null;
            }
        }

        public Product GetProductByID(int id)
        {
            ProDAL<Product> proDAL = new ProDAL<Product>();
            var product = proDAL.GetByFilter(x => x.ProductID == id).SingleOrDefault();
            if (product != null)
            {
                return product;
            }
            else
            {
                return null;
            }
        }

        public Product GetProductByName(string name)
        {
            ProDAL<Product> proDAL = new ProDAL<Product>();
            var product = proDAL.GetByFilter(x => x.ProductName == name).SingleOrDefault();
            if (product != null)
            {
                return product;
            }
            else
            {
                return null;
            }
        }

        public List<ProductListDTO> GetProductFromCategory(string categoryname)
        {
            ProDAL<Product> proDAL = new ProDAL<Product>();
            List<ProductListDTO> productListDto = new List<ProductListDTO>();
            var productList = proDAL.GetByFilter(x => x.Category.CategoryName.Contains(categoryname));
            if (productList.Count > 0)
            {
                foreach (var item in productList)
                {
                    productListDto.Add(MappingConfig.Mapper.Map<ProductListDTO>(item));
                }
                return productListDto;
            }
            else
            {
                return null;
            }

        }

        public List<IngredientCountDTO> GetProductIngredientCount(string ingredient)
        {
            ProDAL<Product> proDAL = new ProDAL<Product>();
            ProDAL<ProductIngredient> productingredientDAL = new ProDAL<ProductIngredient>();
            ProDAL<Ingredient> ingredientDAL = new ProDAL<Ingredient>();
            var ingredientCount = proDAL.GetAll().Join(productingredientDAL.GetEntity(), x => x.ProductID,
                    y => y.ProductID, (p, pi) => new
                    {
                        productid = p.ProductID,
                        productname = p.ProductName,
                        proing = pi
                    }).Join(ingredientDAL.GetEntity(), x => x.proing.IngredientID, y => y.IngredientID, (pi, i) => new
                    {
                        productid = pi.productid,
                        productname = pi.productname,
                        proing = pi.proing,
                        ingredientid = i.IngredientID,
                        ingredientname = i.IngredientName
                    }).Where(x => x.ingredientname == ingredient).GroupBy(x => x.ingredientname).Select(x => new IngredientCountDTO
                    {
                        IngredientName = x.Key,
                        Count = x.Count()
                    }).ToList();
            return ingredientCount;
        }

        public int GetUsersProductCount(int userid)
        {
            ProDAL<Product> proDAL = new ProDAL<Product>();
            ProDAL<User> userDAL = new ProDAL<User>();
            var productCount = proDAL.GetAll().Join(userDAL.GetEntity(), x => x.UserID, y => y.UserID, (p, u) => new
            {
                product = p,
                user = u
            }).Where(x => x.user.UserID == userid).GroupBy(x => x.user).Select(y => y.Count()).ToList();
            return productCount[0];
        }

        public List<ProductListDTO> SearchProductBarcodeName(string barcodename)
        {
            ProDAL<Product> proDAL = new ProDAL<Product>();
            List<ProductListDTO> productListDto = new List<ProductListDTO>();
            var productList = proDAL.GetByFilter(x => x.Barcode.ToString().ToLower().Contains(barcodename.ToLower()));

            foreach (var item in productList)
            {
                productListDto.Add(MappingConfig.Mapper.Map<ProductListDTO>(item));
            }
            if (productList.Count > 0)
            {
                return productListDto;
            }
            else
            {
                return null;
            }
        }

        public Product SearchProductByName(string productname)
        {
            ProDAL<Product> proDAL = new ProDAL<Product>();
            var product = proDAL.GetByFilter(x => x.ProductName.ToLower().Contains(productname.ToLower())).FirstOrDefault();
            return product;
        }

        public List<ProductListDTO> SearchProductName(string productname)
        {
            ProDAL<Product> proDAL = new ProDAL<Product>();
            List<ProductListDTO> productListDto = new List<ProductListDTO>();
            var productList = proDAL.GetByFilter(x => x.ProductName.ToLower().Contains(productname.ToLower()));

            foreach (var item in productList)
            {
                productListDto.Add(MappingConfig.Mapper.Map<ProductListDTO>(item));
            }
            if (productList.Count > 0)
            {
                return productListDto;
            }
            else
            {
                return null;
            }
        }

        public void UpdateProduct(Product product)
        {
            ProDAL<Product> proDAL = new ProDAL<Product>();
            if (product != null)
            {
                proDAL.Update(product);
                proDAL.SaveChanges();
            }

        }
    }
}
