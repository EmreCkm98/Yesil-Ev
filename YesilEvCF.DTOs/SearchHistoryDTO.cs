using System;

namespace YesilEvCF.DTOs
{
    public class SearchHistoryDTO
    {
        public int ProductID { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime SearchDate { get; set; }
    }
}
