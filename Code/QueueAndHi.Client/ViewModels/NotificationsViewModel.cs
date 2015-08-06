using QueueAndHi.BL;
using QueueAndHi.Client.Authentication;
using QueueAndHi.Client.Notifications;
using QueueAndHi.Common.Notifications;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.Client.ViewModels
{
    public class NotificationsViewModel : IDisposable
    {
        private NotificationsObserver notificationsObserver;
        private NotificationService notificationService;

        public NotificationsViewModel()
        {
            Notifications = new ObservableCollection<Notification>();
            this.notificationService = new NotificationService();
            this.notificationsObserver = new NotificationsObserver(this.notificationService);
            if (AuthenticationTokenSingleton.Instance.IsLoggedIn)
            {
                Initialize();
            }

            AuthenticationTokenSingleton.Instance.UserLoggedOut += OnUserLogout;
            AuthenticationTokenSingleton.Instance.UserLoggedIn += OnUserLogIn;
        }

        private void OnUserLogIn(object sender, EventArgs e)
        {
            Initialize();
        }

        private void OnUserLogout(object sender, EventArgs e)
        {
            this.notificationsObserver.NewNotifications -= OnNewNotifications;
            Notifications.Clear();
        }

        private void Initialize()
        {
            Notifications.Clear();
            IEnumerable<Notification> newNotifications = this.notificationService.GetNotifications(AuthenticationTokenSingleton.Instance.AuthenticatedIdentity.Token);
            AddNewNotifications(newNotifications);
            this.notificationsObserver.NewNotifications += OnNewNotifications;
        }

        private void OnNewNotifications(object sender, NewNotificationsEventArgs e)
        {
            AddNewNotifications(e.NewNotifications);
        }

        private void AddNewNotifications(IEnumerable<Notification> newNotifications)
        {
            foreach (Notification notification in newNotifications)
            {
                Notifications.Add(notification);
            }
        }

        public ObservableCollection<Notification> Notifications
        {
            get;
            set;
        }

        public void Dispose()
        {
            OnUserLogout(null, null);
            AuthenticationTokenSingleton.Instance.UserLoggedOut -= OnUserLogout;
            AuthenticationTokenSingleton.Instance.UserLoggedIn -= OnUserLogIn;
        }
    }
}
