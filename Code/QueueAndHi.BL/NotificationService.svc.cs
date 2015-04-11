using QueueAndHi.BL.Notifications;
using QueueAndHi.Common;
using QueueAndHi.Common.Notifications;
using System.Collections.Generic;

namespace QueueAndHi.BL
{
    public class NotificationService : INotificationService
    {
        private INotificationAggregator notificationAggregator;

        public NotificationService()
        {
            this.notificationAggregator = new NotificationAggregator();
        }

        public IEnumerable<Notification> GetNotifications(AuthenticationToken authToken)
        {
            // Get the user-id from the authToken
            // Get all new notifications from the DAL
            // Aggregate the notifications
            // return the aggregated notifications
            return null;
        }
    }
}
