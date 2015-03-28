using QueueAndHi.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace QueueAndHi.BL.Notifications
{
    public class NotificationAggregator : INotificationAggregator
    {
        private ConcurrentDictionary<int, ConcurrentQueue<string>> aggregatedNotifications = new ConcurrentDictionary<int, ConcurrentQueue<string>>();

        public void OnNewNotification(string notification, UserInfo userInfo)
        {
            ConcurrentQueue<string> messagesQueue = GetUserNotificationsQueue(userInfo);
            messagesQueue.Enqueue(notification);
        }

        public void OnNewNotification(string notification, IEnumerable<UserInfo> usersInfo)
        {
            foreach (UserInfo user in usersInfo)
            {
                OnNewNotification(notification, user);
            }
        }

        public bool TryGetUserNotification(UserInfo user, out Notification notification)
        {
            ConcurrentQueue<string> messagesQueue = GetUserNotificationsQueue(user);
            List<string> notificationMessages = new List<string>();
            string message;
            while (messagesQueue.TryDequeue(out message))
            {
                notificationMessages.Add(message);
            }

            // If there are no new notifications to the user
            if (notificationMessages.Count == 0)
            {
                return false;
            }

            StringBuilder notificationStringBuilder = new StringBuilder();
            notificationStringBuilder.Append(notificationMessages[0]);
            for (int i = 1 ; i < notificationMessages.Count ; i++)
            {
                notificationStringBuilder.AppendLine();
                notificationStringBuilder.Append(currentMessage);
            }
            return new Notification { Message = notificationStringBuilder.ToString() };
        }

        private ConcurrentQueue<string> GetUserNotificationsQueue(UserInfo user)
        {
            return this.aggregatedNotifications.GetOrAdd(userInfo.ID, id => new ConcurrentQueue<string>());
        }
    }
}