using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YesilEvCF.Core.Entities
{
    public class Product : EntityBase
    {
        public int ProductID { get; set; }
        [Required]
        [StringLength(50)]
        public string ProductName { get; set; }
        public Nullable<System.Guid> Barcode { get; set; }
        [Required]
        public int ManufacturerID { get; set; }
        public string ProductFrontImage { get; set; }
        public string ProductBackImage { get; set; }
        [Required]
        public int CategoryID { get; set; }
        [Required]
        public int UserID { get; set; }
        public Nullable<bool> ShowUser { get; set; }
        public virtual Category Category { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<ProductIngredient> ProductIngredients { get; set; }
        public virtual ICollection<SearchHistory> SearchHistories { get; set; }
        public virtual ICollection<UserFavorite> UserFavorites { get; set; }
        public virtual ICollection<ProductStatus> ProductStatuses { get; set; }
    }
}
