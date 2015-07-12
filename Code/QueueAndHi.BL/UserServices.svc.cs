using QueueAndHi.Common;
using QueueAndHi.Common.Logic.Validators;
using QueueAndHi.Common.Logic.Validators.User;
using QueueAndHi.Common.Services;
using System.Collections.Generic;

namespace QueueAndHi.BL
{
    public class UserServices : IUserServices
    {
        private IValidator<UserInfo> userValidator;

        public UserServices()
        {
            this.userValidator = new EmailAddressValidator();
            // userValidator = new DecoratedValidator(userValidator);
        }

        public OperationResult AddNewUser(UserInfo newUser, UserCredentials userCredentials)
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

        public OperationResult<AuthenticatedUser> LogIn(UserCredentials userCredentials)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<UserInfo> GetAllUsersData(AuthenticationToken authToken)
        {
            throw new System.NotImplementedException();
        }

        public OperationResult SaveUsersData(AuthenticatedOperation<IEnumerable<UserInfo>> usersData)
        {
            throw new System.NotImplementedException();
        }


        public OperationResult<UserInfo> GetUserInfo(AuthenticationToken authToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
