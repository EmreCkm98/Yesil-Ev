namespace YesilEvCF.DTOs
{
    public class UserProductCategoryIngredientContentDTO
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int UserID { get; set; }
        public int IngredientID { get; set; }
        public string IngredientName { get; set; }
        public string IngredientContent { get; set; }
        public int ContentID { get; set; }
        public string ContentName { get; set; }
        public int ManufacturerID { get; set; }
        public string ManufacturerName { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }
}
