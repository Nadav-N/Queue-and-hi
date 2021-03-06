﻿using QueueAndHi.BL;
using QueueAndHi.Client.Authentication;
using QueueAndHi.Common;
using QueueAndHi.Common.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QueueAndHi.Client.ViewModels
{
    public class QuestionListViewModel : INotifyPropertyChanged
    {
        private IPostQueries postQueries;
        private IPostServices postServices;
        private IUserServices userServices; 
        private NavigationManager navigationManager;

        /// <summary>
        /// loads the page with the provided question list
        /// </summary>
        /// <param name="navigationManager"></param>
        /// <param name="questions"></param>
        public QuestionListViewModel(NavigationManager navigationManager, IPostQueries postQueries, IPostServices postServices, IEnumerable<Question> questions)
        {
            this.postQueries = postQueries;
            this.postServices = postServices;
            this.navigationManager = navigationManager;
            Questions = new ObservableCollection<QuestionInfo>(questions.Select(q => new QuestionInfo(q)));
            NavigateToQuestion = new DelegateCommand(questionInfo => ExecuteNavigateToQuestion(questionInfo));
            DoTagSearch = new DelegateCommand(ExecuteTagSearch);
        }

        /// <summary>
        /// loads the page. the page will load the latest questions
        /// </summary>
        /// <param name="navigationManager"></param>

        public QuestionListViewModel(NavigationManager navigationManager, IPostQueries postQueries, IPostServices postServices, IUserServices userServices)
            : this(navigationManager, postQueries, postServices, postQueries.GetLatestQuestions())
        {
            this.userServices = userServices;
        }

        private void ExecuteNavigateToQuestion(object questionInfo)
        {
            int questionId = ((QuestionInfo)questionInfo).ID;
            DiscussionThread selectedThread = null;
            try
            {
                selectedThread = this.postQueries.GetDiscussionThreadById(questionId);
            }
            catch (InvalidOperationException)
            {
                Questions.Remove((QuestionInfo)questionInfo);
                return;
            }

            QuestionViewModel questionVM = new QuestionViewModel(selectedThread, this.postServices, this.navigationManager, this.postQueries, this.userServices);
            this.navigationManager.RequestNavigation(questionVM);
        }

        public ICommand NavigateToQuestion { get; set; }
        public ICommand DoTagSearch { get; set; }
        
        private void ExecuteTagSearch(object tag)
        {
            //search for the questions and load the question results page
            navigationManager.RequestNavigation(new QuestionListViewModel(navigationManager, postQueries, postServices, postQueries.TagsSearch(tag.ToString())));
        }

        public ObservableCollection<QuestionInfo> Questions
        {
            get;
            set;
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
