using QueueAndHi.Client.Models;
using QueueAndHi.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.Client
{
    public abstract class AbstractPost : INotifyPropertyChanged
    {
        private DateTime datePosted;
        private RankingHistoryModel ranking;
        private UserInfo author;
        private string content;
        protected int id;

        public DateTime DatePosted
        {
            get { return this.datePosted; }
            set
            {
                this.datePosted = value;
                OnPropertyChanged("DatePosted");
            }
        }


        public RankingHistoryModel Ranking
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

        public UserInfo Author
        {
            get
            {
                return this.author;
            }
            set
            {
                this.author = value;
                OnPropertyChanged("Author");
            }
        }

        public string Content
        {
            get { return this.content; }
            set
            {
                this.content = value;
                OnPropertyChanged("Content");
            }
        }

        public int ID
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
                OnPropertyChanged("ID");
            }
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
