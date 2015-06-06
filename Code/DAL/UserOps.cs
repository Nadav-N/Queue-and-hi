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
            UserInfo result = new UserInfo();

            user tmpUser = new user();

            using (var db = new qnhdb())
            {
                tmpUser = db.users.Where(x => x.id == userId).Single();
            }
            
            UserInfo tmpUserInfo;
            UserCredentials tmpUserCredentials;
            tmpUserInfo = Converter.toExtUser(tmpUser, out tmpUserCredentials);
                
            return result;
        }




    }
}
