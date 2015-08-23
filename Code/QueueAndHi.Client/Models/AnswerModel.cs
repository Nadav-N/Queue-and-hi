using QueueAndHi.Client.Authentication;
using QueueAndHi.Client.Models;
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
        private int relatedQuestionId;

        public AnswerModel(int relatedQuestionId)
        {
            this.relatedQuestionId = relatedQuestionId;
            Ranking = new RankingHistoryModel();
        }

        public AnswerModel(Question question, Answer answer)
        {
            this.answered = question.RightAnswerId.HasValue && question.RightAnswerId.Value == answer.ID;
            this.relatedQuestionId = answer.RelatedQuestionId;
            this.id = answer.ID;
            Author = answer.Author;
            Content = answer.Content;
            DatePosted = answer.DatePosted;
            Ranking = new RankingHistoryModel(answer.Ranking);
        }

        public Answer ToExternal()
        {
            return new Answer
            {
                Content = Content,
                Ranking = new RankingHistory(Ranking),
                Author = new UserInfo
                {
                    ID = AuthenticationTokenSingleton.Instance.AuthenticatedIdentity.UserID
                },
                RelatedQuestionId = this.relatedQuestionId,
                DatePosted = DatePosted,
                ID = this.id
            };
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

        public int RelatedQuestionId
        {
            get
            {
                return this.relatedQuestionId;
            }
        }
    }
}
