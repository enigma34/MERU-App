using System;
using System.ComponentModel.DataAnnotations;

namespace Meru_Web.Models
{
    public class ClientKey
    {
        [Key]
        public int ClientKeyId { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public DateTime Createdon { get; set; }
        public int UserId { get; set; }
        public string DeviceId { get; set; }
    }
}