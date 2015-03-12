using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.Client
{
    public class Question:AbstractPost
    {
        private bool recommended;
        private ObservableCollection<string> tags;
        private ObservableCollection<Answer> answers;
        private string title;

        public string Title
        {
            get { return this.title; }
            set
            {
                this.title = value;
                OnPropertyChanged("Title");
            }
        }

        public bool Recommended
        {
            get { return this.recommended; }
            set
            {
                this.recommended = value;
                OnPropertyChanged("Recommended");
            }
        }

        public ObservableCollection<string> Tags
        {
            get
            {
                return this.tags;
            }
            set
            {
                this.tags = value;
                OnPropertyChanged("Tags");
            }
        }

        public ObservableCollection<Answer> Answers
        {
            get
            {
                return this.answers;
            }
            set
            {
                this.answers = value;
                OnPropertyChanged("Answers");
            }
        }
    }
}
