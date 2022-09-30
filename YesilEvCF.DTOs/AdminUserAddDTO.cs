using System;

namespace YesilEvCF.DTOs
{
    public class AdminUserAddDTO
    {
        public string Name { get; set; }

        public string UserSurname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public int RollID { get; set; }
        public Nullable<bool> Premium { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
