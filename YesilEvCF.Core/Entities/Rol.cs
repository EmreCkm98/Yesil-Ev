using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YesilEvCF.Core.Entities
{
    public class Rol : EntityBase
    {
        public int RolID { get; set; }
        [Required]
        [StringLength(20)]
        public string RollName { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
