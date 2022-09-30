using System;

namespace YesilEvCF.DTOs
{
    public class ProductStatusDTO
    {
        public Guid TrackingNumber { get; set; }
        public int ProductID { get; set; }
        public int ApprovementStatusID { get; set; }
        public int? UserID { get; set; }
        public string Detail { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
