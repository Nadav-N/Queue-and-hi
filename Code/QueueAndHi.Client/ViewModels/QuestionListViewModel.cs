using QueueAndHi.BL;
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

        public QuestionListViewModel(NavigationManager navigationManager, IEnumerable<Question> questions)
        {
            this.postQueries = new PostQueries();
            this.postServices = new PostServices();
            this.navigationManager = navigationManager;
            Questions = new ObservableCollection<QuestionInfo>(questions.Select(q => new QuestionInfo(q)));
            NavigateToQuestion = new DelegateCommand(questionInfo => ExecuteNavigateToQuestion(questionInfo));
        }

        private void ExecuteNavigateToQuestion(object questionInfo)
        {
            int questionId = ((QuestionInfo)questionInfo).ID;
            DiscussionThread selectedThread = this.postQueries.GetDiscussionThreadById(questionId);
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
