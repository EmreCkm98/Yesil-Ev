using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using YesilEvCF.Core.Entities;

namespace YesilEvCF.Core.Context
{
    public class YesilEvDbContext : DbContext
    {
        public YesilEvDbContext() : base("name=YesilEv")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Content> Contents { get; set; }
        public virtual DbSet<Ingredient> Ingredients { get; set; }
        public virtual DbSet<Manufacturer> Manufacturers { get; set; }
        public virtual DbSet<ProductIngredient> ProductIngredients { get; set; }
        public virtual DbSet<Rol> Rols { get; set; }
        public virtual DbSet<SearchHistory> SearchHistories { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Favorite> Favorites { get; set; }
        public virtual DbSet<BlackList> BlackLists { get; set; }
        public virtual DbSet<UserFavorite> UserFavorites { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ApprovementStatus> ApprovementStatus { get; set; }
        public virtual DbSet<ProductStatus> ProductStatus { get; set; }
    }
}
