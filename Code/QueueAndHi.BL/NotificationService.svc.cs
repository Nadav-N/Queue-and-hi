using QueueAndHi.BL.Notifications;
using QueueAndHi.Common;
using QueueAndHi.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace QueueAndHi.BL
{
    public class NotificationService : INotificationService
    {
        private INotificationAggregator notificationAggregator;

        public NotificationService()
        {
            this.notificationAggregator = new NotificationAggregator();
        }

        // TODO: We should probably get here something like authentication token of the user
        public GetNotificationResult GetNotification(UserInfo userBeingNotified)
        {
            GetNotificationResult notificationResult = new GetNotificationResult();
            Notification newNotification;
            if (notificationAggregator.TryGetUserNotification(userBeingNotified, out newNotification))
            {
                notificationResult.IsNewNotification = true;
                notificationResult.Notification = newNotification;
            }
            return notificationResult;
        }
    }
}
