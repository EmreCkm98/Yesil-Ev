using System;
using System.Collections.Generic;
using YesilEvCF.Core.Entities;
using YesilEvCF.Core.Interfaces;
using YesilEvCF.DTOs;

namespace YesilEvCF.DAL.Abstract
{
    public interface IProductDAL : IRepo<Product>
    {
        ProductListDTO BarcodeSearch(Guid guid);
        List<ProductListDTO> SearchProductName(string productname);
        List<ProductListDTO> SearchProductBarcodeName(string barcodename);
        List<ProductListDTO> GetProductFromCategory(string categoryname);
        List<Product> GetAllProduct();
        List<Product> GetAllProductByUserID(int id);
        bool AddSearchHistory(SearchHistory searchHistory);
        Product AddProduct(ProductAddDTO productAddDTO);
        Product SearchProductByName(string productname);
        Product GetProductByID(int id);
        Product GetProductByName(string name);
        Product GetProductByBarcode(Guid id);
        void UpdateProduct(Product product);
        Product DeleteProduct(int id);
        List<IngredientCountDTO> GetProductIngredientCount(string ingredient);
        List<ProductIngredientDTO> GetAllUserProductDetail(int id);
        List<ProductFavoriteDTO> GetAllUserFavoriteProduct(int id);
        List<ProductDetailDTO> GetAllUserProductCategory(int userid, int productid);
        List<UserProductCategoryIngredientContentDTO> GetAllProductByCategoryByIngredient(int userid, string ingredient);
        int GetUsersProductCount(int userid);
    }
}
