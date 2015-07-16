using QueueAndHi.Client.Authentication;
using QueueAndHi.Common;
using QueueAndHi.Common.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QueueAndHi.Client.Notifications
{
    public class NotificationsObserver : IDisposable
    {
        private INotificationService notificationService;
        private Timer timer;
        private const int timerDueTime = 20000;

        public NotificationsObserver(INotificationService notificationService)
        {
            this.notificationService = notificationService;
            this.timer = new Timer(CheckForNotifications, null, timerDueTime, Timeout.Infinite);
        }

        private void CheckForNotifications(object state)
        {
            AuthenticationToken authToken = AuthenticationTokenSingleton.Instance.AuthenticatedIdentity.Token;
            IEnumerable<Notification> newNotifications = this.notificationService.GetNotifications(authToken);

            if (newNotifications != null && newNotifications.Count() > 0)
            {
                OnNewNotifications(newNotifications);
            }

            this.timer.Change(timerDueTime, Timeout.Infinite);
        }

        private void OnNewNotifications(IEnumerable<Notification> newNotifications)
        {
            if (NewNotifications != null)
            {
                NewNotifications(this, new NewNotificationsEventArgs(newNotifications));
            }
        }

        public event EventHandler<NewNotificationsEventArgs> NewNotifications;

        public void Dispose()
        {
            this.timer.Dispose();
        }
    }
}
