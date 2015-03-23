using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.Common.Services
{
    public interface INotificationService
    {
        [OperationContract]
        Notification ListenForNotification();
    }
}
