using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QueueAndHi.Common.Notifications;
using QueueAndHi.Common;

namespace DAL
{
    public class NotificationOps
    {
        private UserOps userOps = new UserOps();

        public void SaveNotification(int userId, string message, NotificationType notificationType)
        {
            using (var db = new qnhdb())
            {
                notification notification = new notification
                {
                    message = message,
                    notification_type = (int)notificationType,
                    recipient = userId,
                    seen = Convert.ToByte(false),
                    timestamp = DateTime.Now
                };

                db.notifications.Add(notification);
                db.SaveChanges();
            }
        }

        public IEnumerable<Notification> GetNotifications(int userId)
        {
            using (var db = new qnhdb())
            {
                UserInfo ui = userOps.GetUserInfo(userId);
                return db.notifications.Where(notification => notification.recipient == userId && !Convert.ToBoolean(notification.seen)).Select(n => Converter.toExtNotification(n, ui));
            }
        }
    }
}
