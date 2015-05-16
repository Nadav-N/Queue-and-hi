using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.Client
{
    public class NavigationManager
    {
        event EventHandler<NavigationRequestedEventArgs> NavigationRequested;
    }

    public class NavigationRequestedEventArgs : EventArgs
    {
        public Type TypeToNavigate { get; set; }
    }
}
