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
        public AnswerViewModel(DiscussionThread thread, Answer currentAnswer, IPostServices postServices, IPostQueries postQueries, NavigationManager navigationManager, IUserServices userServices)
            : base(thread, postServices, postQueries, navigationManager, userServices)
        {
            Post = new AnswerModel(discussionThread.Question, currentAnswer);
            MarkAsRight = new DelegateCommand(s => ExecuteMarkAsRight());
            UnmarkAsRight = new DelegateCommand(s => ExecuteUnmarkAsRight());
        }

        public AnswerModel Answer
        {
            get
            {
                return Post;
            }
        }

        public ICommand MarkAsRight { get; set; }

        public ICommand UnmarkAsRight { get; set; }

        public bool IsQuestionAuthorOrAdmin
        {
            get
            {
                return AuthenticationTokenSingleton.Instance.IsLoggedIn && (AuthenticationTokenSingleton.Instance.AuthenticatedUser.IsAdmin ||
                    this.discussionThread.Question.Author.ID == AuthenticationTokenSingleton.Instance.AuthenticatedIdentity.UserID);
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
            base.ExecuteRankUp();
        }

        protected override void ExecuteCancelRankUp()
        {
            this.postServices.CancelAnswerVote(GetAuthenticatedOperation());
            base.ExecuteCancelRankUp();
        }

        protected override void ExecuteRankDown()
        {
            this.postServices.VoteDownAnswer(GetAuthenticatedOperation());
            base.ExecuteRankDown();
        }

        protected override void ExecuteCancelRankDown()
        {
            this.postServices.CancelAnswerVote(GetAuthenticatedOperation());
            base.ExecuteCancelRankDown();
        }

        protected override void ExecuteDelete()
        {
            this.postServices.DeleteAnswer(GetAuthenticatedOperation());
            DiscussionThread dt = this.postQueries.GetDiscussionThreadById(Post.RelatedQuestionId);
            var vm = new QuestionViewModel(dt, this.postServices, this.navigationManager, this.postQueries, this.userServices);
            this.navigationManager.RequestNavigation(vm);
        }
    }
}
