using System;

namespace YesilEvCF.DTOs
{
    public class AdminProductListDTO
    {
        public string ProductName { get; set; }
        public Guid Barcode { get; set; }

        public int ManufacturerID { get; set; }
        public string ProductFrontImage { get; set; }
        public string ProductBackImage { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int CategoryID { get; set; }

        public int UserID { get; set; }
        public bool ShowUser { get; set; }
        public bool IsActive { get; set; }
    }
}
