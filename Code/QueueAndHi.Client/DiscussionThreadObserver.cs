using QueueAndHi.Common;
using QueueAndHi.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.Client
{
    //yeah, yeah, yeah, this is the observable, not the observer...
    public class DiscussionThreadObserver : IDisposable
    {
        //subscribing to the event is the attach method.
        //unsubscribing from it is the detach method.
        public event EventHandler<EventArgs> NewDiscussionThreadVersion;

        //this is not needed, it should be passed in the NewDiscussionThreadEventArgs
        //public DiscussionThread LatestDiscussionThread { get; set; }

        public DiscussionThreadObserver(IPostQueries postQueries)
        {

        }

        //this method starts the thread that monitors the question
        public void StartObservingDiscussionThread(int questionId)
        {
            //will start the thread that polls the DAL. 
            //inside the thread - 
                //if there's a new version - raise the event. 
                //the updated discussion thread will be in the eventargs.
        }


        public void Dispose()
        {
            //dispose will kill the polling thread
            throw new NotImplementedException();
        }
    }
}
