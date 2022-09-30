using System.ComponentModel.DataAnnotations;

namespace YesilEvCF.Core.Entities
{
    public class BlackList : EntityBase
    {
        public int BlackListID { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public int IngredientID { get; set; }

        public virtual Ingredient Ingredient { get; set; }
        public virtual User User { get; set; }
    }
}
