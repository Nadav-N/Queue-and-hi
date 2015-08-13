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

namespace QueueAndHi.Client
{
    /// <summary>
    /// Interaction logic for RegistrationControl.xaml
    /// </summary>
    public partial class RegistrationControl : UserControl
    {
        public RegistrationControl()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            { ((dynamic)this.DataContext).Password = ((PasswordBox)sender).Password; }
            validatePwdMatch();
        }

        private void PasswordBox_ConfirmationChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            { ((dynamic)this.DataContext).Confirmation = ((PasswordBox)sender).Password; }
            validatePwdMatch();
           
        }

        private void validatePwdMatch()
        {
            //validation
            if (txtPwd2.Password != txtPwd.Password)
            {
                //Execute code to alert user passwords don't match here.
                txtPwd2.BorderBrush = Brushes.Red;
                txtPwd.BorderBrush = Brushes.Red;
            }
            else
            {
                /*You must make sure that whatever you do is reversed here;
                 *all users will get the above "error" whilst typing in and you need to make sure
                 *that it goes when they're right!*/
                txtPwd2.BorderBrush = Brushes.Gray;
                txtPwd.BorderBrush = Brushes.Gray;
            }
        }
    }
}
