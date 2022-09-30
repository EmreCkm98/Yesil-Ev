using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YesilEvCF.Core.Entities
{
    public class Ingredient : EntityBase
    {
        public int IngredientID { get; set; }
        [Required]
        [StringLength(50)]
        public string IngredientName { get; set; }
        [Required]
        [StringLength(500)]
        public string IngredientContent { get; set; }
        public virtual ICollection<ProductIngredient> ProductIngredients { get; set; }
        public virtual ICollection<BlackList> BlackLists { get; set; }
    }
}
