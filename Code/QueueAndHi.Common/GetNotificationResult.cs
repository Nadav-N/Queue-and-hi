using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.Common
{
    [DataContract]
    public class GetNotificationResult
    {
        [DataMember]
        public Notification Notification { get; set; }

        [DataMember]
        public bool IsNewNotification { get; set; }
    }
}
