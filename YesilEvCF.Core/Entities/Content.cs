using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YesilEvCF.Core.Entities
{
    public class Content : EntityBase
    {
        public int ContentID { get; set; }
        [Required]
        [StringLength(50)]
        public string ContentName { get; set; }
        public virtual ICollection<ProductIngredient> ProductIngredients { get; set; }
    }
}
