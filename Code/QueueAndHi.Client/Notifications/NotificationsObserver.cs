using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.Client.Notifications
{
    public class NotificationsObserver
    {
        public event EventHandler<NewNotificationsEventArgs> NewNotifications;
    }
}
