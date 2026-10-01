using System;
using System.Collections.Generic;
using System.Text;

namespace IDP.Domain.Entities
{
    public class User : IDP.Domain.Entities.BaseEntities.BaseEnitity
    {

        public required string FullName { get; set; }
        public required string CodeNumber { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string Salt { get; set; }

    }
}
