using System.Collections.Generic;
using YesilEvCF.Core.Entities;
using YesilEvCF.Core.Interfaces;
using YesilEvCF.DTOs;

namespace YesilEvCF.DAL.Abstract
{
    public interface IProductIngredientDAL : IRepo<ProductIngredient>
    {
        ProductIngredient AddProductIngredient(ProductIngredientAddDTO productIngredientAddDTO);
        void UpdateProductIngredient(ProductIngredient productIngredient);
        List<ProductIngredientDTO> GetAllProductIngredients();
        List<ProductBlackListDTO> GetAllUserProductIngredients(int id);
        List<ContentCountDTO> GetAllUserProductIngredientsContentCount(int userid, int productid);
        List<IngredientDTO> GetAllUserProductIngredients(int userid, int productid);
        List<ProductBlackListDTO> GetAllUserProductIngredientsByContent(int id, string name);
        List<ProductIngredient> GetProductIngredientByProductID(int id);
        ProductIngredient GetProductIngredientByProductID(int id, int ingredientId);
        ProductIngredient GetProductIngredientByIngredientID(int id);
        void DeleteProductIngredient(ProductIngredient productIngredient);
    }
}
