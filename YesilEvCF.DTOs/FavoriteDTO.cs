using System;

namespace YesilEvCF.DTOs
{
    public class FavoriteDTO
    {
        public string FavoriteName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
