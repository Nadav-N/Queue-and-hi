using QueueAndHi.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace QueueAndHi.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            MainMenuVM = new MainMenuViewModel();
            MainToolbarVM = new MainToolbarViewModel();
            MainVM = new QuestionListViewModel();
            NotificationsVM = new NotificationsViewModel();

            InitializeComponent();
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
    }
}
