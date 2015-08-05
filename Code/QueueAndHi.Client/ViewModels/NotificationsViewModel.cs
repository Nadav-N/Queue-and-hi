using QueueAndHi.Common.Notifications;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.Client.ViewModels
{
    public class NotificationsViewModel
    {
        public NotificationsViewModel()
        {
            Notifications = new ObservableCollection<Notification>();
        }

        public ObservableCollection<Notification> Notifications
        {
            get;
            set;
        }
    }
}
