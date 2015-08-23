using QueueAndHi.Common;
using QueueAndHi.Common.Logic.Validators;
using QueueAndHi.Common.Logic.Validations.User;
using QueueAndHi.Common.Services;
using System.Collections.Generic;
using QueueAndHi.BL.Authentication;
using System;
using DAL;

namespace QueueAndHi.BL
{
    public class UserServices : IUserServices
    {

        private UserOps userOps = new UserOps();
        private IAuthTokenSerializer authTokenSerializer;
        private IValidator<UserInfo> userValidator;
        private NotificationOps notificationOps;
        
        public UserServices()
        {
            this.authTokenSerializer = new AuthTokenSerializer();
            this.userValidator = new UserValidator();
            this.notificationOps = new NotificationOps();
        }

        public OperationResult AddNewUser(UserInfo newUser, UserCredentials userCredentials)
        {
            newUser.Ranking = 0;
            newUser.IsMuted = false;
            newUser.IsAdmin = false;
            OperationResult validationResult = userValidator.IsValid(newUser);

            //if (validationResult.IsSuccessful)
            //{
                // call DAL to create new user
              //  return new OperationResult();
            //}

            //return validationResult;
            List<string> errorMsgs = new List<string>();
            OperationResult or = new OperationResult(errorMsgs);
            try
            {
                UserInfo newu = userOps.CreateNewUser(newUser, userCredentials);
                return new OperationResult<UserInfo>(newu);
            }
            catch (DAL.Exceptions.DALDuplicateKeyError dke)
            {
                errorMsgs.Add("Failed to create new user.\nA user with this name or email already exists in the system");
                return or;
            }
            catch (Exception ex)
            {
                errorMsgs.Add("Uknow error: " + ex.Message);
                return or;
            }

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
            UserInfo ui = getUserInfo(authToken);
            if (ui.IsAdmin)
            {
                return userOps.GetAllUsers();
            }
            else
            {
                return null;
            }

        }

        public OperationResult SaveUsersData(AuthenticatedOperation<IEnumerable<UserInfo>> usersData)
        {
            UserInfo ui = getUserInfo(usersData.Token);
            if (ui.IsAdmin)
            {
                userOps.SaveUsersData(usersData.Payload);
                
                return new OperationResult<IEnumerable<UserInfo>>(usersData.Payload);
            }
            else
            {
                return new OperationResult(new List<string>() { "User is not admin and is not allowed to change other users" });
            }
        }


        public OperationResult<UserInfo> GetUserInfo(AuthenticationToken authToken)
        {
            UserInfo ui = getUserInfo(authToken);
            return new OperationResult<UserInfo>(ui);
        }

        private UserInfo getUserInfo(AuthenticationToken authToken)
        {
            int userid = authTokenSerializer.Deserialize(authToken);
            UserInfo ui = userOps.GetUserInfo(userid);
            return ui;
        }
    }
}
