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
        // TODO: userinfo should probably be generated on server side from authentication token
        bool TryGetNewNotification(UserInfo userInfo, out Notification notification);
    }
}
