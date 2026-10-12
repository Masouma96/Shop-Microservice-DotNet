using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.BaseEvent
{
    public class BaseEventBus
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public BaseEventBus()
        {
            Id = Guid.NewGuid();
            CreateDate = DateTime.Now;
        }
    }
}
