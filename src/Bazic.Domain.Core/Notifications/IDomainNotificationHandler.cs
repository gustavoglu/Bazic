using Bazic.Domain.Core.Events;
using System.Collections.Generic;

namespace Bazic.Domain.Core.Notifications
{
    public interface IDomainNotificationHandler<T> : IHandler<T> where T : Message
    {
        List<T> GetNotifications();
        bool HasNotification();
    }
}
