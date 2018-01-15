using System;
using System.ComponentModel.DataAnnotations;

namespace Meru_Web.Models
{
    public class UserHistory
    {
        [Key]
        public int UserHistoryId { get; set; }
        public DateTime RequestRecivedAt { get; set; }
        public string RequestRecivedFromLocation { get; set; }
        public string Remarks { get; set; }
    }
}