using QueueAndHi.Common;
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
    public class QuestionViewModel : PostViewModel<QuestionModel>
    {
        public QuestionViewModel(DiscussionThread discussionThread)
            : base(discussionThread)
        {

        }

        public QuestionModel Question
        {
            get;
            set;
        }

        public ICommand AddAnswer { get; set; }

        protected override void ExecuteRankUp()
        {
            throw new NotImplementedException();
        }

        protected override void ExecuteCancelRankUp()
        {
            throw new NotImplementedException();
        }

        protected override void ExecuteRankDown()
        {
            throw new NotImplementedException();
        }

        protected override void ExecuteCancelRankDown()
        {
            throw new NotImplementedException();
        }

        protected override void ExecuteDelete()
        {
            throw new NotImplementedException();
        }
    }
}
