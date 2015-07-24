using QueueAndHi.Client.Authentication;
using QueueAndHi.Common;
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

        public PostViewModel(DiscussionThread discussionThread)
        {
            this.discussionThread = discussionThread;
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

        protected abstract void ExecuteRankUp();

        protected abstract void ExecuteCancelRankUp();

        protected abstract void ExecuteRankDown();

        protected abstract void ExecuteCancelRankDown();

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

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
