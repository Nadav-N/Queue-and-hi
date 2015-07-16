using QueueAndHi.Client.Authentication;
using QueueAndHi.Common;
using QueueAndHi.Common.Services;
using System.ComponentModel;
using System.Windows.Input;

namespace QueueAndHi.Client.ViewModels
{
    public class AnswerViewModel : INotifyPropertyChanged
    {
        IPostServices postServices;

        public AnswerViewModel(IPostServices postServices)
        {
            this.postServices = postServices;
            RankUp = new DelegateCommand(s => ExecuteRankUp(), s => CanExecuteRankUp());
        }

        public AnswerModel Answer
        {
            get;
            set;
        }

        public ICommand RankUp { get; private set; }

        public ICommand RankDown { get; set; }

        public ICommand Delete { get; set; }

        public ICommand MarkAsRight { get; set; }

        public ICommand UnmarkAsRight { get; set; }

        public bool IsMarkAsRightVisible { get; set; }

        public bool IsUnmarkAsRightVisible { get; set; }

        private void ExecuteRankUp()
        {
            AuthenticatedOperation<int> authenticatedOperation = AuthenticationTokenSingleton.Instance.CreateAuthenticatedOperation<int>(Answer.ID);
            this.postServices.VoteUpAnswer(authenticatedOperation);
        }

        private bool CanExecuteRankUp()
        {
            if (AuthenticationTokenSingleton.Instance.IsLoggedIn())
            {
                bool AuthorIsLoggedInUser = AuthenticationTokenSingleton.Instance.AuthenticatedUser.Username == Answer.Author;
                return AuthorIsLoggedInUser;
            }

            return false;
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
