using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.Client
{
    public class UserAccountModel : INotifyPropertyChanged
    {
        private string username;
        private int points;
        private bool isAdmin;
        public bool isMuted;

        public string Username
        {
            get
            {
                return this.username;
            }
            set
            {
                this.username = value;
                OnPropertyChanged("Username");
            }
        }

        public int Points
        {
            get
            {
                return this.points;
            }
            set
            {
                this.points = value;
                OnPropertyChanged("Points");
            }
        }

        public bool IsAdmin
        {
            get
            {
                return this.isAdmin;
            }
            set
            {
                this.isAdmin = value;
                OnPropertyChanged("isAdmin");
            }
        }

        public bool IsMuted
        {
            get
            {
                return this.isMuted;
            }
            set
            {
                this.isMuted = value;
                OnPropertyChanged("IsMuted");
            }
        }


        private void OnPropertyChanged(string propName)
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
