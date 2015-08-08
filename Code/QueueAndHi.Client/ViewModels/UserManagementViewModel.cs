using System;
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

            //get the logged in user token, then get his questions
            IEnumerable<UserInfo> managedUsers = this.userServices.GetAllUsersData(AuthenticationTokenSingleton.Instance.AuthenticatedIdentity.Token);
            Users = new ObservableCollection<UserAccountModel>(managedUsers.Select(q => new UserAccountModel(q)));
            OnPropertyChanged("Users");
        }

        public string ApplyResult { get; private set; }


        public ObservableCollection<UserAccountModel> Users
        {
            get;
            set;
        }

        public ICommand ApplyChanges { get; set; }

        private bool ExecuteApplyChanges()
        {
            AuthenticatedOperation<IEnumerable<UserInfo>> ao = new AuthenticatedOperation<IEnumerable<UserInfo>>(
                AuthenticationTokenSingleton.Instance.AuthenticatedIdentity.Token,
                Users.Select(q => q.ToExternal())
                );
            OperationResult or = userServices.SaveUsersData(ao);
            if (or.IsSuccessful)
            {
                ApplyResult = "Changes saved successfully.";
                OnPropertyChanged("ApplyResult");
                return true;
            }
            else
            {
                foreach (string s in or.ErrorMessages)
                {
                    ApplyResult += s + "\n";
                }
                OnPropertyChanged("ApplyResult");
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
