using QueueAndHi.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using QueueAndHi.Common.Services;
using System.Security;
using QueueAndHi.Client.Authentication;

namespace QueueAndHi.Client.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        IUserServices userServices;
        NavigationManager navManager;
        public LoginViewModel(NavigationManager navigationManager, IUserServices userServices)
        {
            this.navManager = navigationManager;
            this.userServices = userServices;
            LoginUser = new DelegateCommand(s => ExecuteLogin()); 
        }

        public string UserName { get; set; }

        public string Password { private get; set; }

        public string LoginResult { get; private set; }
        
        public ICommand LoginUser { get; set; }

        private bool ExecuteLogin()
        {
            OperationResult loginResult = this.userServices.LogIn(new UserCredentials(UserName, Password));
            if (loginResult.IsSuccessful)
            {
                OperationResult<AuthenticatedIdentity> orai = loginResult as OperationResult<AuthenticatedIdentity>;
                UserInfo loggedInUser = userServices.GetUserInfo(orai.Payload.Token).Payload;


                AuthenticationTokenSingleton.Instance.LogIn(orai.Payload, loggedInUser);
                LoginResult = "Welcome, " + UserName;
                OnPropertyChanged("LoginResult");
                
                return true;
            }
            else
            {
                
                foreach (string s in loginResult.ErrorMessages)
                {
                    LoginResult += s + "\n";
                }
                OnPropertyChanged("LoginResult");
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
