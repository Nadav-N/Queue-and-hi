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
            List<Notification> notifications = new List<Notification>();
            using (var db = new qnhdb())
            {
                UserInfo ui = userOps.GetUserInfo(userId);
                foreach (notification n in db.notifications.Where(x => x.recipient == userId && x.seen == 0))
                {
                    notifications.Add(Converter.toExtNotification(n, ui));
                }
            }
            return notifications;
        }
    }
}
