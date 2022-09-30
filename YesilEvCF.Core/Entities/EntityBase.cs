using System;
using System.ComponentModel.DataAnnotations;

namespace YesilEvCF.Core.Entities
{
    public class EntityBase
    {
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
