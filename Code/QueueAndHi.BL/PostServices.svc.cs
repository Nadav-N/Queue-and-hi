using QueueAndHi.Common;
using QueueAndHi.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace QueueAndHi.BL
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "PostServices" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select PostServices.svc or PostServices.svc.cs at the Solution Explorer and start debugging.
    public class PostServices : IPostServices
    {
        public PostServices()
        {

        }

        public void AddQuestion(AuthenticatedOperation<Question> question)
        {
            throw new NotImplementedException();
        }

        public void DeleteQuestion(AuthenticatedOperation<int> questionId)
        {
            throw new NotImplementedException();
        }

        public void AddAnswer(AuthenticationToken token, int questionId, string content)
        {
            throw new NotImplementedException();
        }

        public void DeleteAnswer(AuthenticatedOperation<int> answerId)
        {
            throw new NotImplementedException();
        }

        public void RecommendQuestion(AuthenticatedOperation<int> questionId)
        {
            throw new NotImplementedException();
        }

        public void UnrecommendQuestion(AuthenticatedOperation<int> questionId)
        {
            throw new NotImplementedException();
        }

        public void VoteUpQuestion(AuthenticatedOperation<int> questionId)
        {
            throw new NotImplementedException();
        }

        public void VoteDownQuestion(AuthenticatedOperation<int> questionId)
        {
            throw new NotImplementedException();
        }

        public void VoteUpAnswer(AuthenticatedOperation<int> answerId)
        {
            throw new NotImplementedException();
        }

        public void VoteDownAnswer(AuthenticatedOperation<int> answerId)
        {
            throw new NotImplementedException();
        }

        public void CancelVoteUpQuestion(AuthenticatedOperation<int> questionId)
        {
            throw new NotImplementedException();
        }

        public void CancelVoteDownQuestion(AuthenticatedOperation<int> questionId)
        {
            throw new NotImplementedException();
        }

        public void CancelVoteUpAnswer(AuthenticatedOperation<int> answerId)
        {
            throw new NotImplementedException();
        }

        public void CancelVoteDownAnswer(AuthenticatedOperation<int> answerId)
        {
            throw new NotImplementedException();
        }

        public void MarkAsRightAnswer(AuthenticatedOperation<int> answerId)
        {
            throw new NotImplementedException();
        }

        public void UnmarkAsRightAnswer(AuthenticatedOperation<int> answerId)
        {
            throw new NotImplementedException();
        }
    }
}
