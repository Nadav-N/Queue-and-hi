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
    public class NewAnswerViewModel : INotifyPropertyChanged
    {
        private IPostServices postServices;
        private int questionId;
        private IValidator<Answer> validator;

        public NewAnswerViewModel(int questionId, IPostServices postServices)
        {
            this.questionId = questionId;
            this.postServices = postServices;
            Answer = new AnswerModel(questionId);
            SubmitAnswer = new DelegateCommand(s => ExecuteAddAnswer());

            this.validator = new ContentValidator();
        }

        public AnswerModel Answer
        {
            get;
            set;
        }

        public void ExecuteAddAnswer()
        {
            OperationResult result = this.validator.IsValid(Answer.ToExternal());
            if (result.IsSuccessful)
            {
                this.postServices.AddAnswer(AuthenticationTokenSingleton.Instance.AuthenticatedIdentity.Token, this.questionId, Answer.Content);
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
