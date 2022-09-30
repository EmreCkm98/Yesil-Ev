using System;

namespace YesilEvCF.DTOs
{
    public class RegisterDTO
    {
        public string Name { get; set; }
        public string UserSurname { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int RollID { get; set; } = 2;
        public bool isActive { get; set; } = true;
        public bool Premium { get; set; } = false;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime ModifiedDate { get; set; } = DateTime.Now;

    }
}
