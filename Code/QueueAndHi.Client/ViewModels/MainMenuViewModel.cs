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

        public MainMenuViewModel(NavigationManager navigationManager, IPostQueries postQueries, IPostServices postServices)
        {
            this.navigationManager = navigationManager;
            this.postQueries = postQueries;
            NavigateNewQuestion = new DelegateCommand(obj => navigationManager.RequestNavigation(new NewQuestionViewModel(postServices)), s => AuthenticationTokenSingleton.Instance.IsLoggedIn);
            NavigateMyQuestions = new DelegateCommand(obj => ExecuteNavigateMyQuestions(), s => AuthenticationTokenSingleton.Instance.IsLoggedIn);
            NavigateUserManagement = new DelegateCommand(obj => navigationManager.RequestNavigation(new UserManagementViewModel()));
            NavigateHome = new DelegateCommand(obj => ExecuteNavigateHome());
            AuthenticationTokenSingleton.Instance.UserLoggedIn += OnUserLoggedIn;
            AuthenticationTokenSingleton.Instance.UserLoggedOut += OnUserLoggedOut;
        }

        private void ExecuteNavigateMyQuestions()
        {
            IEnumerable<Question> myQuestions = this.postQueries.GetMyQuestions(AuthenticationTokenSingleton.Instance.AuthenticatedIdentity.Token);
            this.navigationManager.RequestNavigation(new QuestionListViewModel(this.navigationManager, myQuestions));
        }

        private void ExecuteNavigateHome()
        {
            IEnumerable<Question> latestQuestion = this.postQueries.GetLatestQuestions();
            this.navigationManager.RequestNavigation(new QuestionListViewModel(this.navigationManager, latestQuestion));
        }

        private void OnUserLoggedOut(object sender, EventArgs e)
        {
            IsUserAdmin = false;
            NavigateNewQuestion.RaiseCanExecuteChanged();
            NavigateMyQuestions.RaiseCanExecuteChanged();
        }

        private void OnUserLoggedIn(object sender, EventArgs e)
        {
            IsUserAdmin = AuthenticationTokenSingleton.Instance.AuthenticatedUser.IsAdmin;
            NavigateNewQuestion.RaiseCanExecuteChanged();
            NavigateMyQuestions.RaiseCanExecuteChanged();
        }

        public bool IsUserAdmin
        {
            get
            {
                return this.isUserAdmin;
            }
            set
            {
                this.isUserAdmin = value;
                OnPropertyChanged("IsUserAdmin");
            }
        }

        public DelegateCommand NavigateHome { get; set; }

        public DelegateCommand NavigateMyQuestions { get; set; }

        public DelegateCommand NavigateNewQuestion { get; private set; }

        public DelegateCommand NavigateUserManagement { get; set; }

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
