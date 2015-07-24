using QueueAndHi.Client.Authentication;
using QueueAndHi.Common;
using QueueAndHi.Common.Services;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace QueueAndHi.Client.ViewModels
{
    public class AnswerViewModel : PostViewModel<AnswerModel>
    {
        IPostServices postServices;

        public AnswerViewModel(DiscussionThread thread, Answer currentAnswer, IPostServices postServices)
            : base(thread)
        {
            this.postServices = postServices;
            Post = new AnswerModel(discussionThread.Question, currentAnswer);
            MarkAsRight = new DelegateCommand(s => ExecuteMarkAsRight());
            UnmarkAsRight = new DelegateCommand(s => ExecuteUnmarkAsRight());
        }

        public ICommand MarkAsRight { get; set; }

        public ICommand UnmarkAsRight { get; set; }

        public bool IsQuestionAuthorOrAdmin
        {
            get
            {
                return AuthenticationTokenSingleton.Instance.AuthenticatedUser.IsAdmin ||
                    this.discussionThread.Question.Author.ID == AuthenticationTokenSingleton.Instance.AuthenticatedIdentity.UserID;
            }
        }

        private void ExecuteMarkAsRight()
        {
            this.postServices.MarkAsRightAnswer(GetAuthenticatedOperation());
            Post.Answered = true;
        }

        private void ExecuteUnmarkAsRight()
        {
            this.postServices.UnmarkAsRightAnswer(GetAuthenticatedOperation());
            Post.Answered = false;
        }

        protected override void ExecuteRankUp()
        {
            this.postServices.VoteUpAnswer(GetAuthenticatedOperation());
            int userId = AuthenticationTokenSingleton.Instance.AuthenticatedUser.ID;
            Post.Ranking.RemoveAll(entry => entry.UserID == userId);
            Post.Ranking.Add(new RankingEntry(AuthenticationTokenSingleton.Instance.AuthenticatedUser.ID, RankingType.Up));
            Post.OnPropertyChanged("Ranking");
        }

        protected override void ExecuteCancelRankUp()
        {
            this.postServices.CancelVoteUpAnswer(GetAuthenticatedOperation());
            int userId = AuthenticationTokenSingleton.Instance.AuthenticatedUser.ID;
            Post.Ranking.RemoveAll(entry => entry.UserID == userId && entry.RankingType == RankingType.Up);
            Post.OnPropertyChanged("Ranking");
        }

        protected override void ExecuteRankDown()
        {
            this.postServices.VoteDownAnswer(GetAuthenticatedOperation());
            int userId = AuthenticationTokenSingleton.Instance.AuthenticatedUser.ID;
            Post.Ranking.RemoveAll(entry => entry.UserID == userId);
            Post.Ranking.Add(new RankingEntry(AuthenticationTokenSingleton.Instance.AuthenticatedUser.ID, RankingType.Down));
            Post.OnPropertyChanged("Ranking");
        }

        protected override void ExecuteCancelRankDown()
        {
            this.postServices.CancelVoteDownAnswer(GetAuthenticatedOperation());
            int userId = AuthenticationTokenSingleton.Instance.AuthenticatedUser.ID;
            Post.Ranking.RemoveAll(entry => entry.UserID == userId && entry.RankingType == RankingType.Down);
            Post.OnPropertyChanged("Ranking");
        }

        protected override void ExecuteDelete()
        {
            this.postServices.DeleteAnswer(GetAuthenticatedOperation());
            if (AnswerDeleted != null)
            {
                AnswerDeleted(this, EventArgs.Empty);
            }
        }

        public EventHandler<EventArgs> AnswerDeleted;
    }
}
