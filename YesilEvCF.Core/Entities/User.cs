using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YesilEvCF.Core.Entities
{
    public class User : EntityBase
    {
        public int UserID { get; set; }
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        [Required]
        [StringLength(30)]
        public string UserSurname { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(30)]
        public string Password { get; set; }
        [Required]
        [StringLength(30)]
        public string UserName { get; set; }
        [Required]
        public int RollID { get; set; }
        public Nullable<bool> Premium { get; set; }

        [ForeignKey("RollID")]
        public virtual Rol Rol { get; set; }
        public virtual ICollection<BlackList> BlackLists { get; set; }
        public virtual ICollection<UserFavorite> UserFavorites { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
