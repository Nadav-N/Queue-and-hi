using System.ComponentModel;
using QueueAndHi.Common.Notifications;
using System;
using System.Linq;
using System.Collections.Generic;

namespace QueueAndHi.BL.Notifications
{
    public class NotificationAggregator : INotificationAggregator
    {
        public IEnumerable<Notification> AggregateNotifications(IEnumerable<Notification> notifications)
        {
            List<Notification> aggregatedNotifications = new List<Notification>();
            foreach (NotificationType notificationType in Enum.GetValues(typeof(NotificationType)))
            {
                IEnumerable<Notification> filteredNotifications = notifications.Where(n => n.Type == notificationType);
                if (filteredNotifications.Count() == 1)
                {
                    aggregatedNotifications.Add(filteredNotifications.Single());
                }
                else if (filteredNotifications.Count() > 1)
                {
                    aggregatedNotifications.Add(MergeSameTypeNotifications(notificationType, filteredNotifications));
                }
            }

            return aggregatedNotifications.OrderByDescending(x=>x.TimeStamp);
        }

        private Notification MergeSameTypeNotifications(NotificationType notificationType, IEnumerable<Notification> filteredNotifications)
        {
            string aggregatedNotificationMessage = GetAggregatedNotificationFormat(notificationType, filteredNotifications.Count());
            return new Notification
            {
                Message = aggregatedNotificationMessage,
                Recipient = filteredNotifications.First().Recipient,
                Seen = false,
                TimeStamp = filteredNotifications.OrderByDescending(not => not.TimeStamp).First().TimeStamp,
                Type = notificationType
            };
        }

        private string GetAggregatedNotificationFormat(NotificationType notificationType, int notificationCount)
        {
            switch (notificationType)
            {
                case NotificationType.AnswerMarkedAsRight:
                    return string.Format("There were {0} answers you posted marked as the right answer.", notificationCount);
                case NotificationType.AnswerRankedDown:
                    return string.Format("Different answers you posted were ranked down {0} times.", notificationCount);
                case NotificationType.AnswerRankedUp:
                    return string.Format("Different answers you posted were ranked up {0} times.", notificationCount);
                case NotificationType.MarkedAsLecturer:
                    return "You were marked as a Lecturer";
                case NotificationType.UnmarkedAsLecturer:
                    return "You're lecturer status has been revoked";
                case NotificationType.NewAnswer:
                    return string.Format("{0} answers were posted to your questions", notificationCount);
                case NotificationType.PostWasDeleted:
                    return string.Format("{0} of your posts were deleted", notificationCount);
                case NotificationType.QuestionMarkedAsRecommended:
                    return string.Format("{0} questions you posted were marked as recommended", notificationCount);
                case NotificationType.QuestionMarkedAsUnrecommended:
                    return string.Format("{0} questions you posted were marked as unrecommended", notificationCount);
                case NotificationType.QuestionRankedDown:
                    return string.Format("Different questions you posted were ranked down {0} times.", notificationCount);
                case NotificationType.QuestionRankedUp:
                    return string.Format("Different questions you posted were ranked up {0} times.", notificationCount);
                case NotificationType.YouWereMuted:
                    return "You were muted by an administrator";
                case NotificationType.YouWereUnmuted:
                    return "You were unmuted by an administrator";
                default:
                    throw new InvalidEnumArgumentException("notificationType", (int)notificationType, typeof(NotificationType));
            }
        }
    }
}