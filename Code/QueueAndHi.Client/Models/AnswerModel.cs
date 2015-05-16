using QueueAndHi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.Client
{
    public class AnswerModel : AbstractPost
    {
        private bool answered;

        public AnswerModel()
        {

        }

        public AnswerModel(Answer answer)
        {

        }

        public Answer ToExternal()
        {
            return null;
        }

        public bool Answered
        {
            get { return this.answered; }
            set
            {
                this.answered = value;
                base.OnPropertyChanged("Answered");
            }
        }

    }
}
