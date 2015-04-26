using QueueAndHi.Common.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.Client.Notifications
{
    public class NewNotificationsEventArgs : EventArgs
    {
        public NewNotificationsEventArgs(IEnumerable<Notification> newNotifications)
        {
            NewNotifications = newNotifications;
        }
        public IEnumerable<Notification> NewNotifications { get; private set; }
    }
}
