using QueueAndHi.Common;
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
    public class NewAnswerViewModel : INotifyPropertyChanged
    {
        private IPostServices postServices;

        public NewAnswerViewModel(IPostServices postServices)
        {
            this.postServices = postServices;
            SubmitAnswer = new DelegateCommand(s => this.postServices.AddAnswer(GetAuthenticatedOperation());
        }

        public AnswerModel Answer
        {
            get;
            set;
        }

        public ICommand SubmitAnswer { get; set; }

        protected AuthenticatedOperation<Answer> GetAuthenticatedOperation()
        {
            return AuthenticationTokenSingleton.Instance.CreateAuthenticatedOperation(Answer.ToExternal());
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
