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
    public interface IUserService
    {
        [OperationContract]
        void AddNewUser(UserInfo newUser);

        // TODO: We need to change the return type of this method so that
        // it will return something similar to "Authentication token"
        [OperationContract]
        void LogIn(string username, string password);

        [OperationContract]
        IEnumerable<UserInfo> GetUsersData();

        [OperationContract]
        void SaveUsersData(IEnumerable<UserInfo> usersData);
    }
}
