using Bazic.Domain.Core.Events;
using System;

namespace Bazic.Domain.Core.Notifications
{
    public class DomainNotification : Event
    {
        public DomainNotification(string key, string value)
        {
            Key = key;
            Value = value;
            Version = 1;
            Id = Guid.NewGuid();
        }

        public string Key { get; set; }
        public string Value { get; set; }
        public int Version{ get; set; }
        public Guid Id { get; set; }
    }
}
