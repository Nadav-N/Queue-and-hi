using QueueAndHi.Common;
using QueueAndHi.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QueueAndHi.Client
{
    //yeah, yeah, yeah, this is the observable, not the observer...
    public class DiscussionThreadObserver : IDisposable
    {
        private IPostQueries postQueries;
        private Timer timer;
        private DiscussionThread latestDiscussionThread;
        private const int timerInterval = 20000;

        //subscribing to the event is the attach method.
        //unsubscribing from it is the detach method.
        public event EventHandler<NewDiscussionThreadVersionEventArgs> NewDiscussionThreadVersion;

        public DiscussionThreadObserver(IPostQueries postQueries)
        {
            this.postQueries = postQueries;
        }

        //this method starts the thread that monitors the question
        public void StartObservingDiscussionThread(int questionId)
        {
            this.latestDiscussionThread = this.postQueries.GetDiscussionThreadById(questionId);
            if (this.timer != null)
            {
                this.timer.Dispose();
                this.timer = null;
            }

            this.timer = new Timer(OnTimerProc, null, timerInterval, Timeout.Infinite);
        }

        private void OnTimerProc(object state)
        {
            DiscussionThread newDiscussionThread = this.postQueries.GetDiscussionThreadById(this.latestDiscussionThread.Question.ID);
            if (newDiscussionThread.Version != this.latestDiscussionThread.Version)
            {
                if (NewDiscussionThreadVersion != null)
                {
                    //NewDiscussionThreadVersion(this, new NewDiscussionThreadVersionEventArgs(newDiscussionThread));
                }
            }

            this.timer.Change(timerInterval, Timeout.Infinite);
        }

        public void Dispose()
        {
            this.timer.Dispose();
        }
    }
}
