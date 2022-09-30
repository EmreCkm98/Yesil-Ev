using System.ComponentModel.DataAnnotations;

namespace YesilEvCF.Core.Entities
{
    public class SearchHistory : EntityBase
    {
        public int SearchHistoryID { get; set; }
        [Required]
        public int ProductID { get; set; }
        [Required]
        public System.DateTime SearchDate { get; set; }
        public virtual Product Product { get; set; }
    }
}
