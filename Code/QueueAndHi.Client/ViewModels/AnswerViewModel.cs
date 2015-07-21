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
            RankUp = new DelegateCommand(s => ExecuteRankUp(), s => CheckIsUserAuthorOfAnswer());
            RankDown = new DelegateCommand(s => ExecuteRankDown(), s => CheckIsUserAuthorOfAnswer());
        }

        public AnswerModel Answer
        {
            get;
            set;
        }

        public ICommand RankUp { get; private set; }

        public ICommand CancelRankUp { get; private set; }

        public ICommand RankDown { get; set; }

        public ICommand CancelRankDown { get; private set; }

        public ICommand Delete { get; set; }

        public ICommand MarkAsRight { get; set; }

        public ICommand UnmarkAsRight { get; set; }

        public bool IsAuthor { get; set; }

        public bool IsRankedUp { get; set; }

        public bool IsRankedDown { get; set; }

        public bool IsMarkAsRightVisible { get; set; }

        public bool IsUnmarkAsRightVisible { get; set; }

        private bool CheckIsUserAuthorOfAnswer()
        {
            if (AuthenticationTokenSingleton.Instance.IsLoggedIn())
            {
                bool AuthorIsLoggedInUser = AuthenticationTokenSingleton.Instance.AuthenticatedUser.Username == Answer.Author;
                return AuthorIsLoggedInUser;
            }

            return false;
        }

        private void ExecuteRankUp()
        {
            AuthenticatedOperation<int> authenticatedOperation = AuthenticationTokenSingleton.Instance.CreateAuthenticatedOperation<int>(Answer.ID);
            this.postServices.VoteUpAnswer(authenticatedOperation);
            int userId = AuthenticationTokenSingleton.Instance.AuthenticatedUser.ID;
            Answer.Ranking.RemoveAll(entry => entry.UserID == userId);
            Answer.Ranking.Add(new RankingHistoryEntry(AuthenticationTokenSingleton.Instance.AuthenticatedUser.ID, RankingType.Up));
        }

        private void ExecuteRankDown()
        {
            AuthenticatedOperation<int> authenticatedOperation = AuthenticationTokenSingleton.Instance.CreateAuthenticatedOperation<int>(Answer.ID);
            this.postServices.VoteDownAnswer(authenticatedOperation);
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
