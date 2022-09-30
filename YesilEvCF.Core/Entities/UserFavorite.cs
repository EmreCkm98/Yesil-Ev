namespace YesilEvCF.Core.Entities
{
    public class UserFavorite : EntityBase
    {
        public int UserFavoriteID { get; set; }

        public int UserID { get; set; }

        public int FavoriteID { get; set; }

        public int? ProductID { get; set; }

        public virtual Favorite Favorite { get; set; }
        public virtual User User { get; set; }

        public virtual Product Product { get; set; }
    }
}
