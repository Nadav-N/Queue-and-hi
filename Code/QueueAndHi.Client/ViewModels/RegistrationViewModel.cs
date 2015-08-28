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
using QueueAndHi.Common.Logic.Validators;
using QueueAndHi.Common.Logic.Validations.User;
using QueueAndHi.Client.Authentication;

namespace QueueAndHi.Client.ViewModels
{
    public class RegistrationViewModel : INotifyPropertyChanged
    {
        IUserServices userServices;
        IPostQueries postQueries;
        IPostServices postServices;
        private IValidator<UserInfo> validator;
        private IValidator<Tuple<string, string>> pwdValidator;

        NavigationManager navManager;
        public RegistrationViewModel(NavigationManager navigationManager, IUserServices userServices, IPostQueries postQueries, IPostServices postServices)
        {

            this.navManager = navigationManager;
            this.userServices = userServices;
            this.postQueries = postQueries;
            this.postServices = postServices;
            this.validator = new UserValidator();
            this.pwdValidator = new PasswordValidator();

            RegisterUser = new DelegateCommand(s => ExecuteRegister()); 
        }
        public UserInfo User { get; set; }

        public UserCredentials Credentials { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string Confirmation { get; set; }
        public string Email { get; set; }
        public string RegistrationResult { get; private set; }

        public ICommand RegisterUser { get; set; }

        private bool ExecuteRegister()
        {
            bool validationResult = true;

            RegistrationResult = "";
            //create objects from the user posted data
            this.User = new UserInfo()
            {
                EmailAddress = Email,
                IsAdmin = false,
                IsMuted = false,
                Ranking = 0,
                Username = UserName
            };

            OperationResult or = validator.IsValid(this.User);
            if (!or.IsSuccessful)
            {
                foreach (string s in or.ErrorMessages)
                {
                    RegistrationResult += s + "\n";
                }
                OnPropertyChanged("RegistrationResult");
                validationResult =  false;
            }


            
            //1. that the password and password confirmation fields matched
            //2. that the password follows password guidance rules have passed
            OperationResult por = pwdValidator.IsValid(new Tuple<string,string>(this.Password, this.Confirmation));
            if (por.IsSuccessful)
            {
                this.Credentials = new UserCredentials(UserName, Password);
            }
            else
            {
                foreach (string s in por.ErrorMessages)
                {
                    RegistrationResult += s + "\n";
                }
                OnPropertyChanged("RegistrationResult");
                validationResult = false;
            }

            //make sure all validations passed.
            if (!validationResult) return false;


            OperationResult registerResult = this.userServices.AddNewUser(this.User, this.Credentials);
            if (registerResult.IsSuccessful)
            {
                RegistrationResult = "User " + UserName + " registered successfully.\n You will now be logged in automatically.";
                var authIdentity = userServices.LogIn(this.Credentials).Payload;
                var userInfo = userServices.GetUserInfo(authIdentity.Token).Payload;
                AuthenticationTokenSingleton.Instance.LogIn(authIdentity, userInfo);
                OnPropertyChanged("RegistrationResult");
                Task.Delay(5000).ContinueWith(_ =>
                    {
                        navManager.RequestNavigation(new QuestionListViewModel(navManager, postQueries, postServices, userServices));
                    }
                );
                return true;
            }
            else
            {

                foreach (string s in registerResult.ErrorMessages)
                {
                    RegistrationResult += s + "\n";
                }
                OnPropertyChanged("RegistrationResult");
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
