using System;

namespace YesilEvCF.DTOs
{
    public class UserFavoriteDTO
    {
        public int UserID { get; set; }

        public int FavoriteID { get; set; }

        public int? ProductID { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
