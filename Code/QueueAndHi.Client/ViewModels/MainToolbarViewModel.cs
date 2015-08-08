using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using QueueAndHi.Common.Services;
using QueueAndHi.Client.Authentication;
using QueueAndHi.Common;

namespace QueueAndHi.Client.ViewModels
{
    public class MainToolbarViewModel : INotifyPropertyChanged, IDisposable
    {
        private IPostQueries postQueries;
        private IPostServices postServices;
        private NavigationManager navigationManager;

        public MainToolbarViewModel(NavigationManager navigationManager, IUserServices userServices, IPostQueries postQueries, IPostServices postServices)
        {
            this.postQueries = postQueries;
            this.navigationManager = navigationManager;
            this.postServices = postServices;
            DoLogin = new DelegateCommand(obj => navigationManager.RequestNavigation(new LoginViewModel(navigationManager, userServices, postQueries, postServices)));
            DoRegister = new DelegateCommand(obj => navigationManager.RequestNavigation(new RegistrationViewModel(navigationManager, userServices, postQueries, postServices)));
            DoLogout = new DelegateCommand(obj => ExecuteLogout());
            DoSearch = new DelegateCommand(obj => ExecuteSearch());
            AuthenticationTokenSingleton.Instance.UserLoggedIn += OnUserLogInChanged;
            AuthenticationTokenSingleton.Instance.UserLoggedOut += OnUserLogInChanged;
        }

        private void OnUserLogInChanged(object sender, EventArgs e)
        {
            OnPropertyChanged("IsLoggedIn");
        }

        public bool IsLoggedIn
        {
            get
            {
                return AuthenticationTokenSingleton.Instance.IsLoggedIn;
            }
        }

        public void ExecuteLogout()
        {
            AuthenticationTokenSingleton.Instance.LogOut();
            this.navigationManager.RequestNavigation(new QuestionListViewModel(this.navigationManager, this.postQueries, this.postServices, new List<Question>()));
        }

        public void ExecuteSearch()
        {
            string searchText = SearchText;
            SearchText = String.Empty;
            IEnumerable<Question> result = this.postQueries.FreeSearch(searchText);
            QuestionListViewModel questionList = new QuestionListViewModel(this.navigationManager, this.postQueries, this.postServices, result);
            this.navigationManager.RequestNavigation(questionList);
        }

        public string SearchText { get; set; }

        public ICommand DoSearch { get; set; }

        public ICommand DoLogin { get; set; }

        public ICommand DoRegister { get; set; }

        public ICommand DoLogout { get; set; }

        internal void OnPropertyChanged(string propName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Dispose()
        {
            AuthenticationTokenSingleton.Instance.UserLoggedIn -= OnUserLogInChanged;
            AuthenticationTokenSingleton.Instance.UserLoggedOut -= OnUserLogInChanged;
        }
    }
}
