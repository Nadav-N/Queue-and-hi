using QueueAndHi.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.Client
{
    public class QuestionModel : AbstractPost
    {
        private bool recommended;
        private ObservableCollection<string> tags;
        private ObservableCollection<AnswerModel> answers;
        private string title;
        private int answerCount;

        public QuestionModel()
        {
            this.tags = new ObservableCollection<string>();
            this.answers = new ObservableCollection<AnswerModel>();
        }

        public QuestionModel(DiscussionThread question)
        {
            Author = question.Question.Author;
            DatePosted = question.Question.DatePosted;
            VotesCount = question.Question.Ranking;
            Content = question.Question.Content;
            Title = question.Question.Title;
            Recommended = question.Question.IsRecommended;
            Tags = new ObservableCollection<string>(question.Question.Tags);
            Answers = new ObservableCollection<AnswerModel>(question.Answers);
            AnswerCount = question.Answers.Count();
        }

        public Question ToExternal()
        {
            return new Question
            {
                Author = Author,
                DatePosted = DatePosted,
                Ranking = VotesCount,
                Content = Content,
                Title = Title,
                IsRecommended = Recommended,
                Tags = Tags.ToList(),
                AnswerCount = AnswerCount
            };
        }

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

        public ObservableCollection<AnswerModel> Answers
        {
            get
            {
                return this.answers;
            }
            set
            {
                this.answers = value;
                AnswerCount = this.answers.Count;
                OnPropertyChanged("Answers");
            }
        }

        public int AnswerCount
        {
            get { return this.answerCount; }
            set
            {
                this.answerCount = value;
                OnPropertyChanged("AnswerCount");
            }
        }
    }
}
