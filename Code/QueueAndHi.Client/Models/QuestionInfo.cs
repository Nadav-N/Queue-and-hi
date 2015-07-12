using QueueAndHi.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.Client
{
    public class QuestionInfo : INotifyPropertyChanged
    {
        private int answersCount;
        private int votesCount;
        private string title;
        private string author;
        private bool isRecommended;
        private ObservableCollection<string> tags;
        private int id;

        public QuestionInfo(Question question)
        {
            AnswersCount = question.AnswerCount;
            VotesCount = question.Ranking;
            Title = question.Title;
            Author = question.Author;
            IsRecommended = question.IsRecommended;
            Tags = new ObservableCollection<string>(question.Tags);
            this.id = question.ID;
        }

        public int AnswersCount
        {
            get
            {
                return this.answersCount;
            }
            set
            {
                this.answersCount = value;
                OnPropertyChanged("AnswersCount");
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

        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                this.title = value;
                OnPropertyChanged("Title");
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

        public bool IsRecommended
        {
            get
            {
                return this.isRecommended;
            }
            set
            {
                this.isRecommended = value;
                OnPropertyChanged("IsRecommended");
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
