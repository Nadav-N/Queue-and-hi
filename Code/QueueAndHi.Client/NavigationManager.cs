using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.Client
{
    public class NavigationManager
    {
        public void RequestNavigation(object viewModel)
        {
            throw new NotImplementedException();
        }

        event EventHandler<NavigationRequestedEventArgs> NavigationRequested;
    }

    public class NavigationRequestedEventArgs : EventArgs
    {
        public object ViewModelToNavigate { get; set; }
    }
}
