﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using QueueAndHi.Client.Authentication;
using QueueAndHi.Common;
using QueueAndHi.Common.Services;

namespace QueueAndHi.Client.ViewModels
{
    public class UserManagementViewModel : INotifyPropertyChanged
    {
        IUserServices userServices;
        private IPostQueries postQueries;
        private IPostServices postServices;
        private NavigationManager navigationManager;

        public UserManagementViewModel(NavigationManager navigationManager, IUserServices userServices, IPostQueries postQueries, IPostServices postServices)
        {
            this.userServices = userServices;
            this.postQueries = postQueries;
            this.postServices = postServices;
            this.navigationManager = navigationManager;

            ApplyChanges = new DelegateCommand(s => ExecuteApplyChanges()); 

            //get the logged in user from his token, then get all the users he can manage (meaning if a non admin user
            //somehow got here by mistake - he won't get any users to manage
            IEnumerable<UserInfo> managedUsers = this.userServices.GetAllUsersData(AuthenticationTokenSingleton.Instance.AuthenticatedIdentity.Token);
            Users = new ObservableCollection<UserAccountModel>(managedUsers.Select(q => new UserAccountModel(q)));
            StaleUsers = Users.Select(objEntity => (UserAccountModel)objEntity.Clone()).ToList();
            if (Users.Count == 0)   //counting users as the UserInfo isn't refreshed when the user is changed.
            {
                ApplyResult = "User's Admin privileges where revoked. You are no longer allowed to manage users";
            }
            OnPropertyChanged("Users");
        }

        public string ApplyResult { get; private set; }


        public ObservableCollection<UserAccountModel> Users
        {
            get;
            set;
        }

        private List<UserAccountModel> StaleUsers
        {
            get;
            set;
        }

        public ICommand ApplyChanges { get; set; }

        private bool ExecuteApplyChanges()
        {
            //check which users have changed by comparing the users from "Users" with the users from "StaleUsers"
            //only save changes to the users that have changed (changed means either admin or muted have changed"
            List<UserAccountModel> changedUsers = Users.Where(x => StaleUsers.Any(y => y.id == x.id && (y.isMuted != x.isMuted || y.IsAdmin != x.IsAdmin))).ToList();

            UserAccountModel changedLoggedInUserModel = changedUsers.FirstOrDefault(user => user.ID == AuthenticationTokenSingleton.Instance.AuthenticatedUser.ID);
            if (changedLoggedInUserModel != null)
            {
                changedUsers.Remove(changedLoggedInUserModel);
                UserAccountModel unchangedAdmin = StaleUsers.First(user => user.ID == AuthenticationTokenSingleton.Instance.AuthenticatedUser.ID);

                changedLoggedInUserModel.IsAdmin = unchangedAdmin.IsAdmin;
                changedLoggedInUserModel.IsMuted = unchangedAdmin.IsMuted;
            }

            AuthenticatedOperation<IEnumerable<UserInfo>> ao = new AuthenticatedOperation<IEnumerable<UserInfo>>(
                AuthenticationTokenSingleton.Instance.AuthenticatedIdentity.Token,
                changedUsers.Select(q => q.ToExternal())
                );
            OperationResult or = userServices.SaveUsersData(ao);
            if (or.IsSuccessful)
            {
                //StaleUsers should be updated to current Users after a successful save
                StaleUsers = Users.Select(objEntity => (UserAccountModel)objEntity.Clone()).ToList();
                ApplyResult = "Changes saved successfully.";
                OnPropertyChanged("ApplyResult");
                return true;
            }
            else
            {
                ApplyResult = ""; //clear it from previous messages
                foreach (string s in or.ErrorMessages)
                {
                    ApplyResult += s + "\n";
                }
                OnPropertyChanged("ApplyResult");
                //reset the users to what they were before 
                Users = new ObservableCollection<UserAccountModel>(StaleUsers);
                OnPropertyChanged("Users");
                return false;
            }
        }

        internal void OnPropertyChanged(string propName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
