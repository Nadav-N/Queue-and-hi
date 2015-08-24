using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace QueueAndHi.Common.Services
{
    [ServiceContract]
    public interface IUserServices
    {
        [OperationContract]
        OperationResult AddNewUser(UserInfo newUser, UserCredentials userCredentials);

        [OperationContract]
        OperationResult<AuthenticatedIdentity> LogIn(UserCredentials userCredentials);

        /// <summary>
        /// This method is to get the UserInfo once in a while, in case of rank changes so that the UI will have permission changes
        /// </summary>
        [OperationContract]
        OperationResult<UserInfo> GetUserInfo(AuthenticationToken authToken);

        [OperationContract]
        IEnumerable<UserInfo> GetAllUsersData(AuthenticationToken authToken);

        [OperationContract]
        OperationResult SaveUsersData(AuthenticatedOperation<IEnumerable<UserInfo>> usersData);
    }
}
