using System;
using System.Collections.Generic;
using System.Linq;
using EventBus.BaseEvent;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Events
{
    public class OtpEvents : BaseEventBus
    {
        public string MobileNumber { get; set; } = string.Empty;
        public string OtpCode { get; set; } = string.Empty;
    }
}
