using QueueAndHi.Common;
using QueueAndHi.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.Client
{
    public class DiscussionThreadObserver : IDisposable
    {
        public event EventHandler<EventArgs> NewDiscussionThreadVersion;

        public DiscussionThread LatestDiscussionThread { get; set; }

        public DiscussionThreadObserver(IPostQueries postQueries)
        {

        }

        public DiscussionThread StartObservingDiscussionThread(int questionId)
        {
            return null;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
