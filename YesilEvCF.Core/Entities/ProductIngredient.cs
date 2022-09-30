using System.ComponentModel.DataAnnotations;

namespace YesilEvCF.Core.Entities
{
    public class ProductIngredient : EntityBase
    {
        public int ProductIngredientID { get; set; }
        [Required]
        public int IngredientID { get; set; }
        [Required]
        public int ProductID { get; set; }
        [Required]
        public int ContentID { get; set; }
        public virtual Content Content { get; set; }
        public virtual Ingredient Ingredient { get; set; }
        public virtual Product Product { get; set; }
    }
}
