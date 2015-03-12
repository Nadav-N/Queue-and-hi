using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.Client
{
    public class Answer : AbstractPost
    {
        private bool answered;
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
