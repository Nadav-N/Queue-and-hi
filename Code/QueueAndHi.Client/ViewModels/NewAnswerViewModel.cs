using QueueAndHi.Client.Authentication;
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
        private int questionId;

        public NewAnswerViewModel(int questionId, IPostServices postServices)
        {
            this.questionId = questionId;
            this.postServices = postServices;
            SubmitAnswer = new DelegateCommand(s =>
                this.postServices.AddAnswer(AuthenticationTokenSingleton.Instance.AuthenticatedIdentity.Token, this.questionId, AnswerContent));
        }

        public string AnswerContent
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
