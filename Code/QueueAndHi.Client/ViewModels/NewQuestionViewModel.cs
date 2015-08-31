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
        private IUserServices userServices;
        private IValidator<Question> validator;

        private string tagsText;

        public NewQuestionViewModel(NavigationManager navigationManager, IPostServices postServices, IPostQueries postQueries, IUserServices userServices)
        {
            this.navigationManager = navigationManager;
            this.postServices = postServices;
            this.postQueries = postQueries;
            this.userServices = userServices;
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
            //get the user info for the posting user.
            OperationResult<UserInfo> orui = userServices.GetUserInfo(AuthenticationTokenSingleton.Instance.AuthenticatedIdentity.Token);
            UserInfo ui=null;
            if (orui.IsSuccessful)
            {
                ui = orui.Payload;
            }
            else
            {
                //not logged in? send to login page
                navigationManager.RequestNavigation(new LoginViewModel(navigationManager, userServices, postQueries, postServices));
            }

            Question question = Question.ToExternal();
            //add author to question
            question.Author = ui;

            
            
                OperationResult validationResult = this.validator.IsValid(question);
                if (validationResult.IsSuccessful)
                {
                    try{
                        Question addedQuestion = this.postServices.AddQuestion(AuthenticationTokenSingleton.Instance.CreateAuthenticatedOperation(question));
                        DiscussionThread discussionThread = this.postQueries.GetDiscussionThreadById(addedQuestion.ID);
                        this.navigationManager.RequestNavigation(new QuestionViewModel(discussionThread, this.postServices, this.navigationManager, this.postQueries, this.userServices));
                    }
                    catch (InvalidOperationException ioe)
                    {
                        ErrorMessages = String.Join("\n", ioe.Message);
                        OnPropertyChanged("ErrorMessages");
                    }
                    catch (ArgumentException ae)
                    {
                        ErrorMessages = String.Join("\n", ae.Message);
                        OnPropertyChanged("ErrorMessages");
                    }
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
