using QueueAndHi.BL.Validators;
using QueueAndHi.Common;
using System.Collections.Generic;

namespace QueueAndHi.BL
{
    public class UserServices : IUserServices
    {
        private IUserValidator userValidator;

        public UserServices()
        {
            IUserValidator userValidator = new EmailAddressValidator();
            // userValidator = new DecoratedValidator(userValidator);
        }

        public OperationResult AddNewUser(UserInfo newUser, string password)
        {
            newUser.Ranking = 0;
            newUser.IsMuted = false;
            newUser.IsAdmin = false;
            OperationResult validationResult = userValidator.IsValid(newUser);

            if (validationResult.IsSuccessful)
            {
                // call DAL to create new user
                return new OperationResult();
            }

            return validationResult;
        }

        public OperationResult LogIn(string username, string password)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<UserInfo> GetUsersData()
        {
            throw new System.NotImplementedException();
        }

        public OperationResult SaveUsersData(IEnumerable<UserInfo> usersData)
        {
            throw new System.NotImplementedException();
        }
    }
}
