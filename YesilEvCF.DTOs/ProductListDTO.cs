using System;

namespace YesilEvCF.DTOs
{
    public class ProductListDTO
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public Guid? Barcode { get; set; }
        public bool ShowUser { get; set; }
        public string ProductFrontImage { get; set; }
        public string ProductBackImage { get; set; }
    }
}
