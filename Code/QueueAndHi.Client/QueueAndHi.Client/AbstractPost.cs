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
        private int votesCount;
        //private string title //question only;
        private string author;
        private string content;

        //private bool isRecommended //question only;
        //private bool answered; //answer only
        //private ObservableCollection<string> tags;


        public DateTime DatePosted
        {
            get { return this.datePosted; }
            set
            {
                this.datePosted = value;
                OnPropertyChanged("DatePosted");
            }
        }


        public int VotesCount
        {
            get
            {
                return this.votesCount;
            }
            set
            {
                this.votesCount = value;
                OnPropertyChanged("VotesCount");
            }
        }

        public string Author
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
