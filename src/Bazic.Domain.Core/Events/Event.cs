using System;

namespace Bazic.Domain.Core.Events
{
    public class Event : Message
    {
        public DateTime Timestamp { get; set; }
        public Event()
        {
            Timestamp = DateTime.Now;
        }
    }
}
