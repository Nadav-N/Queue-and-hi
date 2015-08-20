using QueueAndHi.Client.Authentication;
using QueueAndHi.Common;
using QueueAndHi.Common.Logic.Validations.User;
using QueueAndHi.Common.Logic.Validators;
using QueueAndHi.Common.Services;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace QueueAndHi.Client.ViewModels
{
    public class QuestionViewModel : PostViewModel<QuestionModel>, IDisposable
    {
        private NavigationManager navigationManager;
        private DiscussionThreadObserver threadObserver;
        private IValidator<UserInfo> questionRecommendationValidator;

        public QuestionViewModel(DiscussionThread discussionThread, IPostServices postServices, NavigationManager navigationManager, IPostQueries postQueries)
            : base(discussionThread, postServices)
        {
            Post = new QuestionModel(discussionThread);
            this.threadObserver = new DiscussionThreadObserver(postQueries);
            this.threadObserver.StartObservingDiscussionThread(discussionThread.Question.ID);
            this.threadObserver.NewDiscussionThreadVersion += OnNewDiscussionThreadVersion;
            this.navigationManager = navigationManager;
            RecommendQuestion = new DelegateCommand(s => ExecuteRecommendQuestion());
            UnrecommendQuestion = new DelegateCommand(s => ExecuteUnrecommendQuestion());
            AddAnswer = new DelegateCommand(s => ExecuteAddAnswer(), s => AuthenticationTokenSingleton.Instance.IsLoggedIn && !AuthenticationTokenSingleton.Instance.AuthenticatedUser.IsMuted);
            this.questionRecommendationValidator = new RecommendQuestionValidator();
        }

        public bool IsRecommendationEnabled
        {
            get
            {
                return AuthenticationTokenSingleton.Instance.IsLoggedIn && !IsAuthor && this.questionRecommendationValidator.IsValid(AuthenticationTokenSingleton.Instance.AuthenticatedUser).IsSuccessful;
            }
        }

        public ObservableCollection<AnswerViewModel> Answers
        {
            get;
            set;
        }

        private void OnNewDiscussionThreadVersion(object sender, NewDiscussionThreadVersionEventArgs e)
        {
            this.discussionThread = e.NewDiscussionThread;
            Post = new QuestionModel(e.NewDiscussionThread);
            OnPropertyChanged("Post");
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
            this.postServices.CancelQuestionRanking(GetAuthenticatedOperation());
            base.ExecuteCancelRankUp();
        }

        protected override void ExecuteRankDown()
        {
            this.postServices.VoteDownQuestion(GetAuthenticatedOperation());
            base.ExecuteRankDown();
        }

        protected override void ExecuteCancelRankDown()
        {
            this.postServices.CancelQuestionRanking(GetAuthenticatedOperation());
            base.ExecuteCancelRankDown();
        }

        protected override void ExecuteDelete()
        {
            this.postServices.DeleteQuestion(GetAuthenticatedOperation());
            base.ExecuteDelete();
        }

        public void Dispose()
        {
            this.threadObserver.NewDiscussionThreadVersion -= OnNewDiscussionThreadVersion;
            this.threadObserver.Dispose();
        }
    }
}
