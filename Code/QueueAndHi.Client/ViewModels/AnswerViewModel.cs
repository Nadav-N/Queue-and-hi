using QueueAndHi.Client.Authentication;
using QueueAndHi.Common;
using QueueAndHi.Common.Services;
using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Linq;

namespace QueueAndHi.Client.ViewModels
{
    public class AnswerViewModel : PostViewModel<AnswerModel>
    {
        private QuestionViewModel questionVM;

        public AnswerViewModel(DiscussionThread thread, Answer currentAnswer, QuestionViewModel qVM, IPostServices postServices, IPostQueries postQueries, NavigationManager navigationManager, IUserServices userServices)
            : base(thread, postServices, postQueries, navigationManager, userServices)
        {
            this.questionVM = qVM;
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
            AnswerViewModel lastRightAnswer = this.questionVM.Answers.FirstOrDefault(answer => answer.Answer.Answered);
            if (lastRightAnswer != null)
            {
                lastRightAnswer.Answer.Answered = false;
            }

            Post.Answered = true;
            this.postServices.MarkAsRightAnswer(GetAuthenticatedOperation());
        }

        private void ExecuteUnmarkAsRight()
        {
            if (!this.isDisposed)
            {
                Post.Answered = false;
                this.postServices.UnmarkAsRightAnswer(GetAuthenticatedOperation());
            }
        }

        protected override void ExecuteRankUp()
        {
            base.ExecuteRankUp();
            this.postServices.VoteUpAnswer(GetAuthenticatedOperation());
        }

        protected override void ExecuteCancelRankUp()
        {
            base.ExecuteCancelRankUp();
            this.postServices.CancelAnswerVote(GetAuthenticatedOperation());
        }

        protected override void ExecuteRankDown()
        {
            base.ExecuteRankDown();
            this.postServices.VoteDownAnswer(GetAuthenticatedOperation());
        }

        protected override void ExecuteCancelRankDown()
        {
            base.ExecuteCancelRankDown();
            this.postServices.CancelAnswerVote(GetAuthenticatedOperation());
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
