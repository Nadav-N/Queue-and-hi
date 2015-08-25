using QueueAndHi.Client.Authentication;
using QueueAndHi.Common;
using QueueAndHi.Common.Logic.Validations.User;
using QueueAndHi.Common.Logic.Validators;
using QueueAndHi.Common.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QueueAndHi.Client.ViewModels
{
    public abstract class PostViewModel<T> : INotifyPropertyChanged, IDisposable where T : AbstractPost
    {
        protected DiscussionThread discussionThread;
        protected IPostServices postServices;
        protected IPostQueries postQueries;
        protected IUserServices userServices;
        protected NavigationManager navigationManager;
        protected T post;
        protected IValidator<UserInfo> rankDownValidator;
        protected bool isDisposed = false;

        public PostViewModel(DiscussionThread discussionThread, IPostServices postServices, IPostQueries postQueries, NavigationManager navigationManager, IUserServices userServices)
        {
            this.discussionThread = discussionThread;
            this.postServices = postServices;
            this.postQueries = postQueries;
            this.userServices = userServices;
            this.navigationManager = navigationManager;
            RankUp = new DelegateCommand(s => ExecuteRankUp(), s => AuthenticationTokenSingleton.Instance.IsLoggedIn);
            CancelRankUp = new DelegateCommand(s => ExecuteCancelRankUp(), s => AuthenticationTokenSingleton.Instance.IsLoggedIn);
            RankDown = new DelegateCommand(s => ExecuteRankDown(), s => AuthenticationTokenSingleton.Instance.IsLoggedIn);
            CancelRankDown = new DelegateCommand(s => ExecuteCancelRankDown(), s => AuthenticationTokenSingleton.Instance.IsLoggedIn);
            Delete = new DelegateCommand(s => ExecuteDelete(), s => AuthenticationTokenSingleton.Instance.IsLoggedIn && !AuthenticationTokenSingleton.Instance.AuthenticatedUser.IsMuted);
            this.rankDownValidator = new RankDownValidator();
        }

        public T Post
        {
            get
            {
                return this.post;
            }
            set
            {
                this.post = value;
                OnPropertyChanged("Post");
            }
        }


        public ICommand RankUp { get; private set; }

        public ICommand CancelRankUp { get; private set; }

        public ICommand RankDown { get; set; }

        public ICommand CancelRankDown { get; private set; }

        public ICommand Delete { get; set; }

        public bool IsAuthor
        {
            get
            {
                return AuthenticationTokenSingleton.Instance.IsLoggedIn && Post.Author.ID == AuthenticationTokenSingleton.Instance.AuthenticatedIdentity.UserID;
            }
        }

        public bool IsRankUpEnabled
        {
            get
            {
                return AuthenticationTokenSingleton.Instance.IsLoggedIn && !IsAuthor;
            }
        }

        public bool IsRankDownEnabled
        {
            get
            {
                return AuthenticationTokenSingleton.Instance.IsLoggedIn && !IsAuthor && this.rankDownValidator.IsValid(AuthenticationTokenSingleton.Instance.AuthenticatedUser).IsSuccessful;
            }
        }

        /// <summary>
        /// Is the post already ranked up by the current user
        /// Returns false for a not logged in user
        /// </summary>
        public bool IsRankedUp
        {
            get
            {
                return AuthenticationTokenSingleton.Instance.IsLoggedIn && Post.Ranking.Any(entry => entry.UserID == AuthenticationTokenSingleton.Instance.AuthenticatedIdentity.UserID && entry.RankingType == RankingType.Up);
            }
        }

        /// <summary>
        /// Is the post already ranked down by the current user
        /// returns false for a not logged in user
        /// </summary>
        public bool IsRankedDown
        {
            get
            {
                return AuthenticationTokenSingleton.Instance.IsLoggedIn && Post.Ranking.Any(entry => entry.UserID == AuthenticationTokenSingleton.Instance.AuthenticatedIdentity.UserID && entry.RankingType == RankingType.Down);
            }
        }

        public bool CanDeletePost
        {
            get
            {
                return AuthenticationTokenSingleton.Instance.IsLoggedIn && (AuthenticationTokenSingleton.Instance.AuthenticatedUser.IsAdmin || IsAuthor);
            }
        }

        //only a logged in user that's not muted can add an answer
        public bool CanAddAnswer
        {
            get
            {
                return AuthenticationTokenSingleton.Instance.IsLoggedIn && (!AuthenticationTokenSingleton.Instance.AuthenticatedUser.IsMuted);
            }
        }

        protected virtual void ExecuteRankUp()
        {
            int userId = AuthenticationTokenSingleton.Instance.AuthenticatedUser.ID;

            if (post.Ranking.Any(x => x.UserID == userId && x.RankingType == RankingType.Down))
            {
                Post.Ranking.Remove(entry => entry.UserID == userId);
                OnPropertyChanged("IsRankedDown");
            }

            Post.Ranking.Add(new RankingEntry(AuthenticationTokenSingleton.Instance.AuthenticatedUser.ID, RankingType.Up));
            OnPropertyChanged("IsRankedUp");
            Post.OnPropertyChanged("Ranking");
        }

        protected virtual void ExecuteCancelRankUp()
        {
            if (!this.isDisposed)
            {
                int userId = AuthenticationTokenSingleton.Instance.AuthenticatedUser.ID;
                Post.Ranking.Remove(entry => entry.UserID == userId && entry.RankingType == RankingType.Up);
                Post.OnPropertyChanged("Ranking");
                OnPropertyChanged("IsRankedUp");
            }
        }

        protected virtual void ExecuteRankDown()
        {
            int userId = AuthenticationTokenSingleton.Instance.AuthenticatedUser.ID;
            if (post.Ranking.Any(x => x.UserID == userId && x.RankingType == RankingType.Up))
            {
                Post.Ranking.Remove(entry => entry.UserID == userId);
                OnPropertyChanged("IsRankedUp");
            }

            Post.Ranking.Add(new RankingEntry(AuthenticationTokenSingleton.Instance.AuthenticatedUser.ID, RankingType.Down));
            OnPropertyChanged("IsRankedDown");
            Post.OnPropertyChanged("Ranking");
        }

        protected virtual void ExecuteCancelRankDown()
        {
            if (!this.isDisposed)
            {
                int userId = AuthenticationTokenSingleton.Instance.AuthenticatedUser.ID;
                Post.Ranking.Remove(entry => entry.UserID == userId && entry.RankingType == RankingType.Down);
                Post.OnPropertyChanged("Ranking");
                OnPropertyChanged("IsRankedDown");
            }
        }

        protected abstract void ExecuteDelete();

        protected AuthenticatedOperation<int> GetAuthenticatedOperation()
        {
            return AuthenticationTokenSingleton.Instance.CreateAuthenticatedOperation(Post.ID);
        }

        internal void OnPropertyChanged(string propName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propName));
            }
        }

        public virtual void Dispose()
        {
            this.isDisposed = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
