﻿using QueueAndHi.Common;
using QueueAndHi.Common.Logic.Validators;
using QueueAndHi.Common.Logic.Validators.User;
using QueueAndHi.Common.Services;
using System.Collections.Generic;
using QueueAndHi.BL.Authentication;

namespace QueueAndHi.BL
{
    public class UserServices : IUserServices
    {

        private DAL.UserOps userOps = new DAL.UserOps();
        private IAuthTokenSerializer authTokenSerializer;
        private IValidator<UserInfo> userValidator;

        public UserServices(IAuthTokenSerializer authTokenSerializer)
        {
            this.authTokenSerializer = authTokenSerializer;
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

        public OperationResult<AuthenticatedIdentity> LogIn(UserCredentials userCredentials)
        {
            List<string> errorMsg = new List<string>() { "User or password incorrect, please try again." };
            OperationResult<AuthenticatedIdentity> or = new OperationResult<AuthenticatedIdentity>(errorMsg);

            //do login with the credentials against the db using the DAL
            AuthenticatedIdentity ai = new AuthenticatedIdentity();
            bool loginResult = authTokenSerializer.ValidateAndSerialize(userCredentials, out ai);
            if (loginResult == false)//login failure
            {
                return or;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(ai.UserID);
                return new OperationResult<AuthenticatedIdentity>(ai);
            }
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
