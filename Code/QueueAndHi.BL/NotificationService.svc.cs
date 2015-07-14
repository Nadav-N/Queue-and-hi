using QueueAndHi.BL.Authentication;
using QueueAndHi.BL.Notifications;
using QueueAndHi.Common;
using QueueAndHi.Common.Notifications;
using System.Collections.Generic;

namespace QueueAndHi.BL
{
    public class NotificationService : INotificationService
    {
        private IAuthTokenSerializer authTokenSerializer;
        private INotificationAggregator notificationAggregator;

        public NotificationService(IAuthTokenSerializer authTokenSerializer, INotificationAggregator notificationsAggregator)
        {
            this.authTokenSerializer = authTokenSerializer;
            this.notificationAggregator = notificationsAggregator;
        }

        public IEnumerable<Notification> GetNotifications(AuthenticationToken authToken)
        {
            int userId = this.authTokenSerializer.Deserialize(authToken);
            // Get the user-id from the authToken
            // Get all new notifications from the DAL
            // Aggregate the notifications
            // return the aggregated notifications
            return null;
        }
    }
}
