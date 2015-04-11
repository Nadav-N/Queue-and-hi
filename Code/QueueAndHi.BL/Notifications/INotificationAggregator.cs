using QueueAndHi.Common.Notifications;
using System.Collections.Generic;

namespace QueueAndHi.BL.Notifications
{
    public interface INotificationAggregator
    {
        IEnumerable<Notification> AggregateNotifications(IEnumerable<Notification> notifications);
    }
}
