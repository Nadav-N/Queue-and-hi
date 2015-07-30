using QueueAndHi.Client.Authentication;
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
    public class QuestionViewModel : PostViewModel<QuestionModel>, IDisposable
    {
        private NavigationManager navigationManager;
        private DiscussionThreadObserver threadObserver;

        public QuestionViewModel(DiscussionThread discussionThread, IPostServices postServices, NavigationManager navigationManager, IPostQueries postQueries)
            : base(discussionThread, postServices)
        {
            Post = new QuestionModel(discussionThread);
            this.threadObserver = new DiscussionThreadObserver(postQueries);
            this.threadObserver.StartObservingDiscussionThread(discussionThread.Question.ID);
            this.threadObserver.NewDiscussionThreadVersion += OnNewDiscussionThreadVersion;
            this.navigationManager = navigationManager;
            RecommendQuestion = new DelegateCommand(s => ExecuteRecommendQuestion(), s => !Post.IsRecommended);
            UnrecommendQuestion = new DelegateCommand(s => ExecuteUnrecommendQuestion(), s => Post.IsRecommended);
            AddAnswer = new DelegateCommand(s => ExecuteAddAnswer(), s => AuthenticationTokenSingleton.Instance.IsLoggedIn());
        }

        private void OnNewDiscussionThreadVersion(object sender, NewDiscussionThreadVersionEventArgs e)
        {
            this.discussionThread = e.NewDiscussionThread;
            // not sure if this is needed
            OnPropertyChanged("Post.Answers");

        }

        private void ExecuteAddAnswer()
        {
            this.navigationManager.RequestNavigation(new NewAnswerViewModel(Post.ID, this.postServices));
        }

        private void ExecuteRecommendQuestion()
        {
            this.postServices.RecommendQuestion(GetAuthenticatedOperation());
            Post.IsRecommended = true;
        }

        private void ExecuteUnrecommendQuestion()
        {
            this.postServices.UnrecommendQuestion(GetAuthenticatedOperation());
            Post.IsRecommended = false;
        }

        public ICommand RecommendQuestion { get; set; }

        public ICommand UnrecommendQuestion { get; set; }

        public ICommand AddAnswer { get; set; }

        protected override void ExecuteRankUp()
        {
            this.postServices.VoteUpQuestion(GetAuthenticatedOperation());
            base.ExecuteRankUp();
        }

        protected override void ExecuteCancelRankUp()
        {
            this.postServices.CancelVoteUpQuestion(GetAuthenticatedOperation());
            base.ExecuteCancelRankUp();
        }

        protected override void ExecuteRankDown()
        {
            this.postServices.VoteDownQuestion(GetAuthenticatedOperation());
            base.ExecuteRankDown();
        }

        protected override void ExecuteCancelRankDown()
        {
            this.postServices.CancelVoteDownQuestion(GetAuthenticatedOperation());
            base.ExecuteCancelRankDown();
        }

        protected override void ExecuteDelete()
        {
            this.postServices.DeleteQuestion(GetAuthenticatedOperation());
            base.ExecuteDelete();
        }

        public void Dispose()
        {
            this.threadObserver.Dispose();
        }
    }
}
