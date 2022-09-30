using System.Collections.Generic;
using YesilEvCF.Core.Entities;
using YesilEvCF.Core.Interfaces;
using YesilEvCF.DTOs;

namespace YesilEvCF.DAL.Abstract
{
    public interface IIngredientDAL : IRepo<Ingredient>
    {
        List<IngredientListDTO> GetAllIngredients();
        List<IngredientListDTO> GetAllUserIngredients(int id, string name);
        IngredientListDTO GetAllUserIngredientsByIngredientName(int id, string productName, string Ingredientname);
        bool IngredientAnyExist(string ingredientname);
        Ingredient GetIngredientByFilter(string ingredientname);
        Ingredient GetIngredientByContentID(int id);
        Ingredient AddIngredient(IngredientAddDTO ingredientAddDTO);
        void UpdateIngredient(Ingredient ingredient);
    }
}
