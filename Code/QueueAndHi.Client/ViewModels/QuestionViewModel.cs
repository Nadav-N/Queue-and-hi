using QueueAndHi.Client.Authentication;
using QueueAndHi.Common;
using QueueAndHi.Common.Logic.Validations.User;
using QueueAndHi.Common.Logic.Validators;
using QueueAndHi.Common.Services;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace QueueAndHi.Client.ViewModels
{
    public class QuestionViewModel : PostViewModel<QuestionModel>
    {
        private DiscussionThreadObserver threadObserver;
        private IValidator<UserInfo> questionRecommendationValidator;

        public QuestionViewModel(DiscussionThread discussionThread, IPostServices postServices, NavigationManager navigationManager, IPostQueries postQueries, IUserServices userServices)
            : base(discussionThread, postServices, postQueries, navigationManager, userServices)
        {
            Post = new QuestionModel(discussionThread);
            Answers = new ObservableCollection<AnswerViewModel>(discussionThread.Answers.Select(answer => new AnswerViewModel(discussionThread, answer, postServices, postQueries, navigationManager, userServices)));
            this.threadObserver = new DiscussionThreadObserver(postQueries);
            this.threadObserver.StartObservingDiscussionThread(discussionThread.Question.ID);
            this.threadObserver.NewDiscussionThreadVersion += OnNewDiscussionThreadVersion;
            RecommendQuestion = new DelegateCommand(s => ExecuteRecommendQuestion());
            UnrecommendQuestion = new DelegateCommand(s => ExecuteUnrecommendQuestion());
            AddAnswer = new DelegateCommand(s => ExecuteAddAnswer(), s => AuthenticationTokenSingleton.Instance.IsLoggedIn && !AuthenticationTokenSingleton.Instance.AuthenticatedUser.IsMuted);
            this.questionRecommendationValidator = new RecommendQuestionValidator();
        }

        public bool IsRecommendationEnabled
        {
            get
            {
                return AuthenticationTokenSingleton.Instance.IsLoggedIn && (!IsAuthor || AuthenticationTokenSingleton.Instance.AuthenticatedUser.IsAdmin) && this.questionRecommendationValidator.IsValid(AuthenticationTokenSingleton.Instance.AuthenticatedUser).IsSuccessful;
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
            this.navigationManager.RequestNavigation(new NewAnswerViewModel(Post.ID, this.navigationManager, this.userServices, this.postQueries, this.postServices));
        }

        private void ExecuteRecommendQuestion()
        {
            this.postServices.RecommendQuestion(GetAuthenticatedOperation());
            Post.IsRecommended = true;
        }

        private void ExecuteUnrecommendQuestion()
        {
            if (!this.isDisposed)
            {
                this.postServices.UnrecommendQuestion(GetAuthenticatedOperation());
                Post.IsRecommended = false;
            }
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
            if (!this.isDisposed)
            {
                this.postServices.CancelQuestionRanking(GetAuthenticatedOperation());
                base.ExecuteCancelRankUp();
            }
        }

        protected override void ExecuteRankDown()
        {
            this.postServices.VoteDownQuestion(GetAuthenticatedOperation());
            base.ExecuteRankDown();
        }

        protected override void ExecuteCancelRankDown()
        {
            if (!this.isDisposed)
            {
                this.postServices.CancelQuestionRanking(GetAuthenticatedOperation());
                base.ExecuteCancelRankDown();
            }
        }

        protected override void ExecuteDelete()
        {
            //make sure the thread observer doesn't try to get new data about this question, as we're about to delete it
            this.threadObserver.Dispose();
            //delete the question
            this.postServices.DeleteQuestion(GetAuthenticatedOperation());
            //hwow to handle the page view?
            //navigate to the question list view?
            //notify the user?
            //do we do it here, or in the OnPostDeleted Event?
            this.navigationManager.RequestNavigation(new QuestionListViewModel(this.navigationManager, this.postQueries, this.postServices, this.userServices));
        }

        public override void Dispose()
        {
            this.threadObserver.NewDiscussionThreadVersion -= OnNewDiscussionThreadVersion;
            this.threadObserver.Dispose();
            base.Dispose();
        }
    }
}
