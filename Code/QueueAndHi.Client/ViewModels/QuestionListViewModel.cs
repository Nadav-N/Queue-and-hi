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
        private NavigationManager navigationManager;

        public QuestionListViewModel(NavigationManager navigationManager, IPostQueries postQueries, IPostServices postServices, IEnumerable<QuestionInfo> questions)
        {
            this.postQueries = postQueries;
            this.postServices = postServices;
            this.navigationManager = navigationManager;
            Questions = new ObservableCollection<QuestionInfo>(questions);
            NavigateToQuestion = new DelegateCommand(questionInfo => ExecuteNavigateToQuestion(questionInfo));
        }

        private void ExecuteNavigateToQuestion(object questionInfo)
        {
            int questionId = ((QuestionInfo)questionInfo).ID;
            AuthenticatedOperation<int> authOperation = AuthenticationTokenSingleton.Instance.CreateAuthenticatedOperation(questionId);
            DiscussionThread selectedThread = this.postQueries.GetDiscussionThreadById(authOperation);
            QuestionViewModel questionVM = new QuestionViewModel(selectedThread, this.postServices, this.navigationManager, this.postQueries);
            this.navigationManager.RequestNavigation(questionVM);
        }

        public ICommand NavigateToQuestion { get; set; }

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
