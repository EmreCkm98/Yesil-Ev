using System;

namespace YesilEvCF.DTOs
{
    public class IngredientAddDTO
    {
        public string IngredientName { get; set; }

        public string IngredientContent { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
