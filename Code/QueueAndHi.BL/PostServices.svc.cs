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
        public void AddQuestion(AuthenticatedOperation<Question> question)
        {
            throw new NotImplementedException();
        }

        public void DeleteQuestion(AuthenticatedOperation<Question> question)
        {
            throw new NotImplementedException();
        }

        public void AddAnswer(AuthenticatedOperation<Answer> answer)
        {
            throw new NotImplementedException();
        }

        public void DeleteAnswer(AuthenticatedOperation<Answer> answer)
        {
            throw new NotImplementedException();
        }

        public void VoteUpPost(AuthenticatedOperation<IPost> post)
        {
            throw new NotImplementedException();
        }

        public void VoteDownPost(AuthenticatedOperation<IPost> post)
        {
            throw new NotImplementedException();
        }

        public void RecommendQuestion(AuthenticatedOperation<Question> question)
        {
            throw new NotImplementedException();
        }
    }
}
