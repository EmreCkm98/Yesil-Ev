using System;

namespace YesilEvCF.DTOs
{
    public class AddBlackListDTO
    {
        public int UserID { get; set; }
        public int IngredientID { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
