using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.Common.Services
{
    [ServiceContract]
    public interface INotificationService
    {
        [OperationContract]
        GetNotificationResult GetNotification(AuthenticatedOperation<UserInfo> userBeingNotified);
    }
}
