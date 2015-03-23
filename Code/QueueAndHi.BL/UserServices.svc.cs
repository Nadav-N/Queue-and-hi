using QueueAndHi.Common;

namespace QueueAndHi.BL
{
    public class UserServices : IUserServices
    {
        public void AddNewUser(UserInfo newUser, string password)
        {
            newUser.Ranking = 0;
            newUser.IsMuted = false;
            newUser.IsAdmin = false;
            // We need to insert the validation logic of the email address, name and passwords here preferably with a design pattern
        }

        public void LogIn(string username, string password)
        {
            throw new System.NotImplementedException();
        }

        public System.Collections.Generic.IEnumerable<UserInfo> GetUsersData()
        {
            throw new System.NotImplementedException();
        }

        public void SaveUsersData(System.Collections.Generic.IEnumerable<UserInfo> usersData)
        {
            throw new System.NotImplementedException();
        }
    }
}
