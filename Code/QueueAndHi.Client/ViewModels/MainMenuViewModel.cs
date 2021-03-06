﻿using QueueAndHi.Client.Authentication;
using QueueAndHi.Common;
using QueueAndHi.Common.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QueueAndHi.Client.ViewModels
{
    public class MainMenuViewModel : INotifyPropertyChanged, IDisposable
    {
        private bool isUserAdmin;
        NavigationManager navigationManager;
        IPostQueries postQueries;
        IPostServices postServices;
        IUserServices userServices;

        public MainMenuViewModel(NavigationManager navigationManager, IUserServices userServices,  IPostQueries postQueries, IPostServices postServices)
        {
            this.navigationManager = navigationManager;
            this.postQueries = postQueries;
            this.postServices = postServices;
            this.userServices = userServices;
            NavigateNewQuestion = new DelegateCommand(obj => navigationManager.RequestNavigation(new NewQuestionViewModel(navigationManager, postServices, postQueries, userServices)));
            NavigateMyQuestions = new DelegateCommand(obj => ExecuteNavigateMyQuestions());
            NavigateUserManagement = new DelegateCommand(obj => navigationManager.RequestNavigation(new UserManagementViewModel(navigationManager, userServices, postQueries, postServices)));
            NavigateHome = new DelegateCommand(obj => ExecuteNavigateHome());
            AuthenticationTokenSingleton.Instance.UserLoggedIn += OnUserLoggedIn;
            AuthenticationTokenSingleton.Instance.UserLoggedOut += OnUserLoggedOut;
        }

        private void ExecuteNavigateMyQuestions()
        {
            IEnumerable<Question> myQuestions = this.postQueries.GetMyQuestions(AuthenticationTokenSingleton.Instance.AuthenticatedIdentity.Token);
            this.navigationManager.RequestNavigation(new QuestionListViewModel(this.navigationManager,this.postQueries,this.postServices, myQuestions));
        }

        private void ExecuteNavigateHome()
        {
            IEnumerable<Question> latestQuestion = this.postQueries.GetLatestQuestions();
            this.navigationManager.RequestNavigation(new QuestionListViewModel(this.navigationManager, this.postQueries, this.postServices, latestQuestion));
        }

        private void OnUserLoggedOut(object sender, EventArgs e)
        {
            OnPropertyChanged("IsUserAdmin");
            OnPropertyChanged("IsLoggedIn");
            OnPropertyChanged("IsMuted");
        }

        private void OnUserLoggedIn(object sender, EventArgs e)
        {
            OnPropertyChanged("IsUserAdmin");
            OnPropertyChanged("IsLoggedIn");
            OnPropertyChanged("IsMuted");
        }

        public bool IsLoggedIn
        {
            get
            {
                return AuthenticationTokenSingleton.Instance.IsLoggedIn;
            }
        }

        public bool IsMuted
        {
            get
            {
                return !IsLoggedIn || AuthenticationTokenSingleton.Instance.AuthenticatedUser.IsMuted;
            }
        }

        public bool IsUserAdmin
        {
            get
            {
                return AuthenticationTokenSingleton.Instance.IsLoggedIn && AuthenticationTokenSingleton.Instance.AuthenticatedUser.IsAdmin;
            }
        }

        public ICommand NavigateHome { get; set; }

        public ICommand NavigateMyQuestions { get; set; }

        public ICommand NavigateNewQuestion { get; private set; }

        public ICommand NavigateUserManagement { get; set; }

        internal void OnPropertyChanged(string propName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propName));
            }
        }

        public void Dispose()
        {
            AuthenticationTokenSingleton.Instance.UserLoggedIn -= OnUserLoggedIn;
            AuthenticationTokenSingleton.Instance.UserLoggedOut -= OnUserLoggedOut;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
