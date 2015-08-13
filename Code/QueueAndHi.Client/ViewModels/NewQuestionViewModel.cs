using QueueAndHi.Client.Authentication;
using QueueAndHi.Common;
using QueueAndHi.Common.Logic.Validations.Question;
using QueueAndHi.Common.Logic.Validators;
using QueueAndHi.Common.Services;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace QueueAndHi.Client.ViewModels
{
    public class NewQuestionViewModel : INotifyPropertyChanged
    {
        private NavigationManager navigationManager;
        private IPostQueries postQueries;
        private IPostServices postServices;
        private IValidator<Question> validator;

        private string tagsText;

        public NewQuestionViewModel(NavigationManager navigationManager, IPostServices postServices, IPostQueries postQueries)
        {
            this.navigationManager = navigationManager;
            this.postServices = postServices;
            this.postQueries = postQueries;
            Question = new QuestionModel();
            PublishQuestion = new DelegateCommand(s => ValidateAndSubmitQuestion());
            AddNewTag = new DelegateCommand(AddTag);
            RemoveTag = new DelegateCommand(tag => Question.Tags.Remove(tag.ToString()));

            this.validator = new ContentValidator();
            this.validator = new TitleValidator(this.validator);
        }

        private void AddTag(object tag)
        {
            Question.Tags.Add(tag.ToString());
            TagsText = String.Empty;
        }

        public string TagsText
        {
            get
            {
                return this.tagsText;
            }
            set
            {
                this.tagsText = value;
                OnPropertyChanged("TagsText");
            }
        }

        public string ErrorMessages
        {
            get;
            set;
        }

        public ICommand AddNewTag { get; set; }

        public ICommand RemoveTag { get; set; }

        public ICommand PublishQuestion { get; set; }

        public QuestionModel Question { get; set; }

        public void ValidateAndSubmitQuestion()
        {
            Question question = Question.ToExternal();
            OperationResult validationResult = this.validator.IsValid(question);
            if (validationResult.IsSuccessful)
            {
                Question addedQuestion = this.postServices.AddQuestion(AuthenticationTokenSingleton.Instance.CreateAuthenticatedOperation(question));
                DiscussionThread discussionThread = this.postQueries.GetDiscussionThreadById(addedQuestion.ID);
                this.navigationManager.RequestNavigation(new QuestionViewModel(discussionThread, this.postServices, this.navigationManager, this.postQueries));
            }
            else
            {
                ErrorMessages = String.Join("\n", validationResult.ErrorMessages);
                OnPropertyChanged("ErrorMessages");
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
