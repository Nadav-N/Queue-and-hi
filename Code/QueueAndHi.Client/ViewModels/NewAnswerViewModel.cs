using QueueAndHi.Client.Authentication;
using QueueAndHi.Common;
using QueueAndHi.Common.Logic.Validations.Question;
using QueueAndHi.Common.Logic.Validators;
using QueueAndHi.Common.Services;
using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Threading.Tasks;

namespace QueueAndHi.Client.ViewModels
{
    public class NewAnswerViewModel : INotifyPropertyChanged
    {
        private IPostServices postServices;
        private IUserServices userServices;
        private IPostQueries postQueries;
        private NavigationManager navManager;

        private int questionId;
        private IValidator<Answer> validator;

        public NewAnswerViewModel(int questionId, NavigationManager navigationManager, IUserServices userServices, IPostQueries postQueries, IPostServices postServices)
        {
            this.questionId = questionId;
            this.postServices = postServices;
            this.navManager = navigationManager;
            this.userServices = userServices;
            this.postQueries = postQueries;
            Answer = new AnswerModel(questionId);
            SubmitAnswer = new DelegateCommand(s => ExecuteAddAnswer());

            this.validator = new ContentValidator();
        }

        public AnswerModel Answer
        {
            get;
            set;
        }

        private void ExecuteAddAnswer()
        {
            OperationResult result = this.validator.IsValid(Answer.ToExternal());
            if (result.IsSuccessful)
            {
                this.postServices.AddAnswer(AuthenticationTokenSingleton.Instance.CreateAuthenticatedOperation(new Tuple<int, string>(this.questionId, Answer.Content)));
                ErrorMessages = "Answer posted successfully";
                OnPropertyChanged("ErrorMessages");
                Task.Delay(3000).ContinueWith(_ =>
                {
                    DiscussionThread selectedThread = this.postQueries.GetDiscussionThreadById(questionId);
                    navManager.RequestNavigation(new QuestionViewModel(selectedThread, this.postServices, this.navManager, this.postQueries, this.userServices));
                });
            }
            else
            {
                ErrorMessages = String.Join("\n", result.ErrorMessages);
                OnPropertyChanged("ErrorMessages");
            }
        }

        public string ErrorMessages
        {
            get;
            set;
        }

        public ICommand SubmitAnswer { get; set; }

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
