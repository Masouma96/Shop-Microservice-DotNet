using System;
using System.Collections.Generic;
using System.Text;

namespace IDP.Domain.DTO
{
    public class Otp
    {
        public required Int64 UserId { get; set; }
        public required string OtpCode { get; set; }
        public bool IsUsed { get; set; }
    }
}
