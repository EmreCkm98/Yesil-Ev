using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YesilEvCF.Core.Entities
{
    public class Favorite : EntityBase
    {
        public int FavoriteID { get; set; }
        [Required]
        [StringLength(50)]
        public string FavoriteName { get; set; }
        public virtual ICollection<UserFavorite> UserFavorites { get; set; }
    }
}
