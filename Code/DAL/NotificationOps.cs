using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QueueAndHi.Common.Notifications;

namespace DAL
{
    public class NotificationOps
    {
        public void SaveNotification(int userId, string message, NotificationType notificationType)
        {
            using (var db = new qnhdb())
            {
                notification notification = new notification
                {
                    message = message,
                    notification_type = (int)notificationType,
                    recipient = userId,
                    seen = 0,
                    timestamp = DateTime.Now
                };

                db.notifications.Add(notification);
            }
        }

        public IEnumerable<Notification> GetNotifications(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
