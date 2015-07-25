using QueueAndHi.Client.Authentication;
using QueueAndHi.Common;
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
    public abstract class PostViewModel<T> : INotifyPropertyChanged where T : AbstractPost
    {
        protected DiscussionThread discussionThread;
        protected IPostServices postServices;

        public PostViewModel(DiscussionThread discussionThread, IPostServices postServices)
        {
            this.discussionThread = discussionThread;
            this.postServices = postServices;
            RankUp = new DelegateCommand(s => ExecuteRankUp());
            CancelRankUp = new DelegateCommand(s => ExecuteCancelRankUp());
            RankDown = new DelegateCommand(s => ExecuteRankDown());
            CancelRankDown = new DelegateCommand(s => ExecuteCancelRankDown());
            Delete = new DelegateCommand(s => ExecuteDelete());
        }

        public T Post
        {
            get;
            set;
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
                return Post.Author.ID == AuthenticationTokenSingleton.Instance.AuthenticatedIdentity.UserID;
            }
        }

        public bool IsRankedUp
        {
            get
            {
                return Post.Ranking.Any(entry => entry.UserID == AuthenticationTokenSingleton.Instance.AuthenticatedIdentity.UserID && entry.RankingType == RankingType.Up);
            }
        }

        public bool IsRankedDown
        {
            get
            {
                return Post.Ranking.Any(entry => entry.UserID == AuthenticationTokenSingleton.Instance.AuthenticatedIdentity.UserID && entry.RankingType == RankingType.Down);
            }
        }

        public bool CanDeletePost
        {
            get
            {
                return AuthenticationTokenSingleton.Instance.AuthenticatedUser.IsAdmin || IsAuthor;
            }
        }

        protected virtual void ExecuteRankUp()
        {
            int userId = AuthenticationTokenSingleton.Instance.AuthenticatedUser.ID;
            Post.Ranking.RemoveAll(entry => entry.UserID == userId);
            Post.Ranking.Add(new RankingEntry(AuthenticationTokenSingleton.Instance.AuthenticatedUser.ID, RankingType.Up));
            Post.OnPropertyChanged("Ranking");
            OnPropertyChanged("IsRankedUp");
            OnPropertyChanged("IsRankedDown");
        }

        protected virtual void ExecuteCancelRankUp()
        {
            int userId = AuthenticationTokenSingleton.Instance.AuthenticatedUser.ID;
            Post.Ranking.RemoveAll(entry => entry.UserID == userId && entry.RankingType == RankingType.Up);
            Post.OnPropertyChanged("Ranking");
            OnPropertyChanged("IsRankedUp");
        }

        protected virtual void ExecuteRankDown()
        {
            int userId = AuthenticationTokenSingleton.Instance.AuthenticatedUser.ID;
            Post.Ranking.RemoveAll(entry => entry.UserID == userId);
            Post.Ranking.Add(new RankingEntry(AuthenticationTokenSingleton.Instance.AuthenticatedUser.ID, RankingType.Down));
            Post.OnPropertyChanged("Ranking");
            OnPropertyChanged("IsRankedUp");
            OnPropertyChanged("IsRankedDown");
        }

        protected virtual void ExecuteCancelRankDown()
        {
            int userId = AuthenticationTokenSingleton.Instance.AuthenticatedUser.ID;
            Post.Ranking.RemoveAll(entry => entry.UserID == userId && entry.RankingType == RankingType.Down);
            Post.OnPropertyChanged("Ranking");
            OnPropertyChanged("IsRankedDown");
        }

        protected virtual void ExecuteDelete()
        {
            OnPostDeleted();
        }

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

        public event PropertyChangedEventHandler PropertyChanged;

        public EventHandler<EventArgs> PostDeleted;

        protected void OnPostDeleted()
        {
            if (PostDeleted != null)
            {
                PostDeleted(this, EventArgs.Empty);
            }
        }
    }
}
