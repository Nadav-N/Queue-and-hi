using DAL;
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
        private NotificationOps notificationOps;

        public NotificationService()
        {
            this.authTokenSerializer = new AuthTokenSerializer();
            this.notificationAggregator = new NotificationAggregator();
            this.notificationOps = new NotificationOps();
        }

        public IEnumerable<Notification> GetNotifications(AuthenticationToken authToken)
        {
            int userId = this.authTokenSerializer.Deserialize(authToken);
            IEnumerable<Notification> allNotifications = this.notificationOps.GetNotifications(userId);
            return this.notificationAggregator.AggregateNotifications(allNotifications);
        }
    }
}
