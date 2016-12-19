using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsPbMobile.Models
{
    public class EventViewModel
    {
        public Event Event { get; set; }

        public TimeSpan Countdown => Event.Date.Subtract(DateTime.Now);
    }
}
