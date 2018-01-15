using System;

namespace MedInFHIR.Models
{
    public class TokenModel
    {
        public string TokenId { get; set; }
        public string TokenKey { get; set; }
        public DateTime IssueOn { get; set; }
        public DateTime ExpiersOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public string DeviceId { get; set; }
    }
}