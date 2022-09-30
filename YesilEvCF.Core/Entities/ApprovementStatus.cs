using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YesilEvCF.Core.Entities
{
    public class ApprovementStatus : EntityBase
    {
        public int ApprovementStatusID { get; set; }
        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        public virtual ICollection<ProductStatus> ProductStatuses { get; set; }
    }
}
