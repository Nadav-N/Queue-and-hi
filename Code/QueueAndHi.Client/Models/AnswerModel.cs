﻿using QueueAndHi.Client.Authentication;
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
        }

        public AnswerModel(Question question, Answer answer)
        {
            this.answered = question.RightAnswerId.HasValue && question.RightAnswerId.Value == answer.ID;
            this.relatedQuestionId = answer.RelatedQuestionId;
            this.id = answer.ID;
            Author = answer.Author.Username;
            Content = answer.Content;
            DatePosted = answer.DatePosted;
            VotesCount = answer.Ranking;
        }

        public Answer ToExternal()
        {
            return new Answer
            {
                Content = Content,
                Ranking = VotesCount,
                Author = new UserInfo
                {
                    ID = AuthenticationTokenSingleton.Instance.AuthenticatedUser.UserID
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
    }
}