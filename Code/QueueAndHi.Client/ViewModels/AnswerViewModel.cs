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
            RankUp = new DelegateCommand(s => ExecuteRankUp());
            CancelRankUp = new DelegateCommand(s => ExecuteCancelRankUp());
            RankDown = new DelegateCommand(s => ExecuteRankDown());
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

        private AuthenticatedOperation<int> GetAuthenticatedOperation()
        {
            return AuthenticationTokenSingleton.Instance.CreateAuthenticatedOperation<int>(Answer.ID);
        }

        private bool CheckIsUserAuthorOfAnswer()
        {
            if (AuthenticationTokenSingleton.Instance.IsLoggedIn())
            {
                bool AuthorIsLoggedInUser = AuthenticationTokenSingleton.Instance.AuthenticatedUser.ID == Answer.Author.ID;
                return AuthorIsLoggedInUser;
            }

            return false;
        }

        private void ExecuteRankUp()
        {
            this.postServices.VoteUpAnswer(GetAuthenticatedOperation());
            int userId = AuthenticationTokenSingleton.Instance.AuthenticatedUser.ID;
            Answer.Ranking.RemoveAll(entry => entry.UserID == userId);
            Answer.Ranking.Add(new RankingEntry(AuthenticationTokenSingleton.Instance.AuthenticatedUser.ID, RankingType.Up));
            Answer.OnPropertyChanged("Ranking");
        }

        private void ExecuteCancelRankUp()
        {
            this.postServices.CancelVoteUpAnswer(GetAuthenticatedOperation());
            int userId = AuthenticationTokenSingleton.Instance.AuthenticatedUser.ID;
            Answer.Ranking.RemoveAll(entry => entry.UserID == userId && entry.RankingType == RankingType.Up);
            Answer.OnPropertyChanged("Ranking");
        }

        private void ExecuteRankDown()
        {
            this.postServices.VoteDownAnswer(GetAuthenticatedOperation());
            int userId = AuthenticationTokenSingleton.Instance.AuthenticatedUser.ID;
            Answer.Ranking.RemoveAll(entry => entry.UserID == userId);
            Answer.Ranking.Add(new RankingEntry(AuthenticationTokenSingleton.Instance.AuthenticatedUser.ID, RankingType.Down));
            Answer.OnPropertyChanged("Ranking");
        }

        private void ExecuteCancelRankDown()
        {
            this.postServices.CancelVoteDownAnswer(GetAuthenticatedOperation());
            int userId = AuthenticationTokenSingleton.Instance.AuthenticatedUser.ID;
            Answer.Ranking.RemoveAll(entry => entry.UserID == userId && entry.RankingType == RankingType.Down);
            Answer.OnPropertyChanged("Ranking");
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
