using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QueueAndHi.Common;

namespace DAL
{
    public class UserOps
    {
        public UserInfo CreateNewUser(UserInfo userInfo, UserCredentials userCredentials)
        {
            throw new NotImplementedException();
        }

        public bool Login(UserCredentials userCredentials)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserInfo> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public void SaveUserData(UserInfo userinfo)
        {
            throw new NotImplementedException();
        }

        public void SaveUsersData(IEnumerable<UserInfo> usersInfo)
        {
            foreach (UserInfo userInfo in usersInfo)
            {
                SaveUserData(userInfo);
            }
        }

        public UserInfo GetUserInfo(int userId)
        {
            throw new NotImplementedException();
        }




    }
}
