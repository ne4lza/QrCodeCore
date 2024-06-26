﻿using System.ComponentModel.DataAnnotations;

namespace QrCodeCore.Models
{
    public class SysUsers
    {
        [Key]
        public int User_Id { get; set; }
        public string? User_Name { get; set; }
        public string? User_LastName { get; set; }
        public string? User_FullName { get; set; }
        public string? User_UserName { get; set; }
        public string? User_Password { get; set; }
        public bool User_Status { get; set; }
        public int User_BusinessId { get; set; }
    }
}
