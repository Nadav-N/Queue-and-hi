using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using QueueAndHi.Common.Services;

namespace QueueAndHi.Client.ViewModels
{
    public class MainToolbarViewModel : INotifyPropertyChanged
    {
        public MainToolbarViewModel(NavigationManager navigationManager, IUserServices userServices, IPostQueries postQueries, IPostServices postServices)
        {
            DoLogin = new DelegateCommand(obj => navigationManager.RequestNavigation(new LoginViewModel(navigationManager, userServices, postQueries, postServices)));
            DoRegister = new DelegateCommand(obj => navigationManager.RequestNavigation(new RegistrationViewModel(navigationManager, userServices, postQueries, postServices)));
        }

        public string SearchText { get; set; }

        public ICommand DoSearch { get; set; }

        public ICommand DoLogin { get; set; }

        public ICommand DoRegister { get; set; }

        public ICommand DoLogout { get; set; }

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
