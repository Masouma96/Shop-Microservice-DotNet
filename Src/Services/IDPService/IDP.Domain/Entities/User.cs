using System;
using System.Collections.Generic;
using System.Text;

namespace IDP.Domain.Entities
{
    public class User : IDP.Domain.Entities.BaseEntities.BaseEnitity
    {

        public  string? FullName { get; set; }
        public  string? CodeNumber { get; set; }
        public required string UserName { get; set; }
        public  string? Password { get; set; }
        public  string? Salt { get; set; }

        public required string MobileNumber { get; set; }

    }
}
