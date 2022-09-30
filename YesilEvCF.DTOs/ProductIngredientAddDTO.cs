using System;

namespace YesilEvCF.DTOs
{
    public class ProductIngredientAddDTO
    {
        public int IngredientID { get; set; }
        public int ProductID { get; set; }
        public int ContentID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
