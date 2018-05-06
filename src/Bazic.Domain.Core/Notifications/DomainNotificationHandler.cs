using Bazic.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bazic.Domain.Core.Notifications
{
    public class DomainNotificationHandler : IDomainNotificationHandler<DomainNotification> ,IDisposable
    {
        private List<DomainNotification> Notifications;

        public DomainNotificationHandler()
        {
            Notifications = new List<DomainNotification>();
        }

        public List<DomainNotification> GetNotifications()
        {
            return Notifications;
        }

        public void Handler(DomainNotification message)
        {
            Notifications.Add(message);
        }

        public bool HasNotification()
        {
            return Notifications.Any();
        }

        public void Dispose()
        {
            Notifications = new List<DomainNotification>();
        }
    }
}
