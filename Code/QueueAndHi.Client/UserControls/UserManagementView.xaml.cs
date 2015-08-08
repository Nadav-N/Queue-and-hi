using QueueAndHi.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
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
using QueueAndHi.BL;

namespace QueueAndHi.Client
{
    /// <summary>
    /// Interaction logic for UserManagementView.xaml
    /// </summary>
    public partial class UserManagementView : UserControl
    {
        UserServices usrSvc;

        public UserManagementView()
        {
            InitializeComponent();

            //get the current user's authentication token.
            //usrSvc = new UserServices();
            
            
        }
    }
}
