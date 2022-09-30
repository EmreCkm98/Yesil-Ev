using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YesilEvCF.Core.Entities
{
    public class Manufacturer : EntityBase
    {
        public int ManufacturerID { get; set; }
        [Required]
        [StringLength(50)]
        public string ManufacturerName { get; set; }
        [Required]
        public string Phone { get; set; }
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
