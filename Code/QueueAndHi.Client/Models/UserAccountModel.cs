using QueueAndHi.Common;
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
        private int ranking;
        private bool isAdmin;
        public bool isMuted;

        public UserAccountModel()
        {

        }

        public UserAccountModel(UserInfo userInfo)
        {
            Username = userInfo.Username;
            Ranking = userInfo.Ranking;
            IsAdmin = userInfo.IsAdmin;
            IsMuted = userInfo.IsMuted;
        }

        public UserInfo ToExternal()
        {
            return new UserInfo
            {
                Username = Username,
                Ranking = Ranking,
                IsAdmin = IsAdmin,
                IsMuted = IsMuted
            };
        }

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

        public int Ranking
        {
            get
            {
                return this.ranking;
            }
            set
            {
                this.ranking = value;
                OnPropertyChanged("Ranking");
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
