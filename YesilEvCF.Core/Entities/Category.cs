using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YesilEvCF.Core.Entities
{
    public class Category : EntityBase
    {
        public int CategoryID { get; set; }
        [Required]
        [StringLength(50)]
        public string CategoryName { get; set; }
        public Nullable<int> TopCategoryID { get; set; }
        [ForeignKey("TopCategoryID")]
        public Category TopCategory { get; set; }
        public virtual ICollection<Category> TopCategories { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
