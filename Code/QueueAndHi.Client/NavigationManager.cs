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
            if (NavigationRequested != null)
            {
                NavigationRequested(this, new NavigationRequestedEventArgs(viewModel));
            }
        }

        public event EventHandler<NavigationRequestedEventArgs> NavigationRequested;
    }

    public class NavigationRequestedEventArgs : EventArgs
    {
        public NavigationRequestedEventArgs(object viewModel)
        {
            ViewModelToNavigate = viewModel;
        }

        public object ViewModelToNavigate { get; private set; }
    }
}
