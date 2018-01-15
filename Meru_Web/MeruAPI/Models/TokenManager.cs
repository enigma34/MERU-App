using System;
using System.ComponentModel.DataAnnotations;

namespace Meru_Web.Models
{
    public class TokenManager
    {
        [Key]
        public int TokenId { get; set; }
        public string TokenKey { get; set; }
        public DateTime IssueOn { get; set; }
        public DateTime ExpiersOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public string DeviceId { get; set; }
    }
}