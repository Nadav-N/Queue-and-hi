using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace QueueAndHi.Common
{
    [ServiceContract]
    public interface IUserServices
    {
        [OperationContract]
        OperationResult AddNewUser(UserInfo newUser, string password);

        // TODO: We need to change the return type of this method so that
        // it will return something similar to "Authentication token"
        [OperationContract]
        OperationResult LogIn(string username, string password);

        [OperationContract]
        IEnumerable<UserInfo> GetUsersData();

        [OperationContract]
        OperationResult SaveUsersData(IEnumerable<UserInfo> usersData);
    }
}
