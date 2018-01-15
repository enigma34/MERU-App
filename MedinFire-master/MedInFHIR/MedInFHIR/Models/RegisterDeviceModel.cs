using System;

namespace MedInFHIR.Models
{
    public class RegisterDeviceModel
        {
            public int ClientKeyId { get; set; }
            public string ClientId { get; set; }
            public string ClientSecret { get; set; }
            public DateTime Createdon { get; set; }
            public int UserId { get; set; }
            public string DeviceId { get; set; }
        }
}