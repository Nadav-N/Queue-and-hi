using QueueAndHi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.BL.Notifications
{
    public interface INotificationAggregator
    {
        void OnNewNotification(string notification, UserInfo userInfo);

        void OnNewNotification(string notification, IEnumerable<UserInfo> userInfo);

        bool TryGetUserNotification(UserInfo user, out Notification notification);
    }
}
