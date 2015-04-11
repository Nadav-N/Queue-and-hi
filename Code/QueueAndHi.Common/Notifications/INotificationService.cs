using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.Common.Notifications
{
    [ServiceContract]
    public interface INotificationService
    {
        [OperationContract]
        IEnumerable<Notification> GetNotifications(AuthenticationToken authToken);
    }
}
