using QueueAndHi.Common;
using QueueAndHi.Common.Logic.Validators;
using QueueAndHi.Common.Logic.Validations.User;
using QueueAndHi.Common.Services;
using System.Collections.Generic;
using QueueAndHi.BL.Authentication;
using System;
using DAL;
using QueueAndHi.Common.Notifications;

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

            if (!validationResult.IsSuccessful)
            {
                throw new ArgumentException(string.Join(",\n", validationResult.ErrorMessages));
            }

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
                return new List<UserInfo>();
            }

        }

        public OperationResult SaveUsersData(AuthenticatedOperation<IEnumerable<UserInfo>> usersData)
        {
            UserInfo ui = getUserInfo(usersData.Token);
            if (ui.IsAdmin)
            {
                //foreach user changed, pull old date from db, compare them, and send notification of impending change
                foreach (var newu in usersData.Payload)
                {
                    var oldu = userOps.GetUserInfo(newu.ID);
                    userOps.SaveUserData(newu);
                    foreach (Notification noti in userChangeMessages(oldu, newu))
                    {
                        this.notificationOps.SaveNotification(newu.ID,
                        noti.Message,
                        noti.Type);
                    }
                }
                return new OperationResult<IEnumerable<UserInfo>>(usersData.Payload);
            }
            else
            {
                return new OperationResult(new List<string>() { "User is not admin and is not allowed to change other users" });
            }
        }

        private IEnumerable<Notification> userChangeMessages(UserInfo olduser, UserInfo newuser)
        {
            List<Notification> msgs = new List<Notification>();
            string msg = "";
            NotificationType type = NotificationType.AnswerMarkedAsRight;

            if (olduser.IsAdmin && !newuser.IsAdmin){
                msg = "Your Admin privileges have been revoked.";
                type = NotificationType.UnmarkedAsLecturer;
                msgs.Add(new Notification() { Message = msg, Type = type });
            }
            if (!olduser.IsAdmin && newuser.IsAdmin)
            {
                msg = "You have been granted Admin privileges. With great power comes great responsibility.";
                type = NotificationType.MarkedAsLecturer;
                msgs.Add(new Notification() { Message = msg, Type = type });
            }
            if (olduser.IsMuted && !newuser.IsMuted)
            {
                msg = "Your ban has been lifted. You are no longer muted.";
                type = NotificationType.YouWereUnmuted;
                msgs.Add(new Notification() { Message = msg, Type = type });
            }
            if (!olduser.IsMuted && newuser.IsMuted)
            {
                msg = "You have been banned from asking questions or writing answers.";
                type = NotificationType.YouWereMuted;
                msgs.Add(new Notification() { Message = msg, Type = type });
            }
            
            return msgs;
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
