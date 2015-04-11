using QueueAndHi.Common;
using QueueAndHi.Common.Logic.Validations.Question;
using QueueAndHi.Common.Logic.Validators;
using QueueAndHi.Common.Notifications;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace QueueAndHi.BL.Notifications
{
    public class NotificationAggregator : INotificationAggregator
    {
        public IEnumerable<Notification> AggregateNotifications(IEnumerable<Notification> notifications)
        {            
            return null;
        }
    }
}