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
using System.Windows.Shapes;

namespace QueueAndHi.Client.Samples
{
    /// <summary>
    /// Interaction logic for UserManagement.xaml
    /// </summary>
    public partial class UserManagement : Window
    {
        public UserManagement()
        {
            InitializeComponent();
            Users = new ObservableCollection<UserAccount>
            {
                new UserAccount
                {
                    Username = "Nadav",
                    Points = 4
                },
                new UserAccount
                {
                    Username = "Danny",
                    IsAdmin = true,
                    Points = 134
                },
                new UserAccount
                {
                    Username = "Tomer",
                    Points = 12
                },
                new UserAccount
                {
                    Username = "Dr. Evil",
                    Points = -27,
                    IsMuted = true
                }
            };
        }

        public ObservableCollection<UserAccount> Users { get; set; }
    }
}
