using QueueAndHi.Client.Authentication;
using QueueAndHi.Common.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QueueAndHi.Client.ViewModels
{
    public class NewQuestionViewModel : INotifyPropertyChanged
    {
        private IPostServices postServices;

        public NewQuestionViewModel(IPostServices postServices)
        {
            this.postServices = postServices;
            Question = new QuestionModel();
            PublishQuestion = new DelegateCommand(s =>
                postServices.AddQuestion(AuthenticationTokenSingleton.Instance.CreateAuthenticatedOperation(Question.ToExternal()));
        }

        public ICommand PublishQuestion { get; set; }

        public QuestionModel Question { get; set; }

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
