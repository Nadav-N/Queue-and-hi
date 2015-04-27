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
        public DiscussionThread NewDiscussionThread { get; set; }
    }
}
