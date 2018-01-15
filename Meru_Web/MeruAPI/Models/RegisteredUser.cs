using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meru_Web.Models
{
    public class RegisteredUser
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime CreatedOn { get; set; }
        public Boolean ActiveStatus { get; set; }
        public int UserType { get; set; }
    }
}