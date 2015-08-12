using QueueAndHi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.Client
{
    public class NewDiscussionThreadVersionEventArgs : EventArgs
    {
        public NewDiscussionThreadVersionEventArgs(DiscussionThread discussionThread)
        {
            NewDiscussionThread = discussionThread;
        }

        public DiscussionThread NewDiscussionThread { get; private set; }
    }
}
