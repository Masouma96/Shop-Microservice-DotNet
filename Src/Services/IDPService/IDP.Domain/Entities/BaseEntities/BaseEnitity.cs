using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IDP.Domain.Entities.BaseEntities
{
    public class BaseEnitity
    {
        public BaseEnitity()
        {
            this.CreateDate = DateTime.UtcNow;
        }
        [Key]
        public Int64 ID { get; set; }
         public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }

    }
}
