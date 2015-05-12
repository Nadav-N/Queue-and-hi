using QueueAndHi.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.Client.ViewModels
{
    [Export]
    public class RegistrationViewModel : INotifyPropertyChanged
    {
        public UserInfo User { get; set; }
        public UserCredentials Credentials { get; set; }

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
