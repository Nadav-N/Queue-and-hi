using QueueAndHi.BL;
using QueueAndHi.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using QueueAndHi.Common;
using QueueAndHi.Common.Notifications;
using QueueAndHi.BL.Authentication;

namespace QueueAndHi.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            NavigationManager navigationManager = new NavigationManager();
            MainMenuVM = new MainMenuViewModel(navigationManager, new UserServices(), new PostQueries(), new PostServices());
            MainToolbarVM = new MainToolbarViewModel(navigationManager, new UserServices(), new PostQueries(), new PostServices());
            MainVM = new QuestionListViewModel(navigationManager,new PostQueries(), new PostServices(), new PostQueries().GetLatestQuestions());
            NotificationsVM = new NotificationsViewModel();
            navigationManager.NavigationRequested += navigationManager_NavigationRequested;

            InitializeComponent();
        }

        void navigationManager_NavigationRequested(object sender, NavigationRequestedEventArgs e)
        {
            object oldVM = MainVM;
            MainVM = e.ViewModelToNavigate;
            IDisposable disposableVM = oldVM as IDisposable;

            if (disposableVM != null)
            {
                disposableVM.Dispose();
            }

            OnPropertyChanged("MainVM");
        }

        public object MainVM
        {
            get;
            set;
        }

        public NotificationsViewModel NotificationsVM
        {
            get;
            set;
        }

        public MainMenuViewModel MainMenuVM
        {
            get;
            set;
        }

        public MainToolbarViewModel MainToolbarVM
        {
            get;
            set;
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
