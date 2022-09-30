using System;

namespace YesilEvCF.DTOs
{
    public class ManifactureAddDTO
    {
        public string ManufacturerName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
