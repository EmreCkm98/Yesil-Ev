using System;
using System.ComponentModel.DataAnnotations;

namespace YesilEvCF.Core.Entities
{
    public class ProductStatus : EntityBase
    {
        [Key]
        public Guid TrackingNumber { get; set; }
        [Required]
        public int ProductID { get; set; }
        [Required]
        public int ApprovementStatusID { get; set; }
        public int? UserID { get; set; }
        [Required]
        [StringLength(200)]
        public string Detail { get; set; }
        public virtual Product Product { get; set; }
        public virtual ApprovementStatus ApprovementStatus { get; set; }
        public virtual User User { get; set; }
    }
}
