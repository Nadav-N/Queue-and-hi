using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.Common.Notifications
{
    [DataContract]
    public class Notification
    {
        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public bool IsNewNotification { get; set; }

        [DataMember]
        public NotificationType Type { get; set; }
    }
}
