using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.Common.Services
{
    [ServiceContract]
    public interface IPostServices
    {
        [OperationContract]
        void AddQuestion(AuthenticatedOperation<Question> question);

        [OperationContract]
        void DeleteQuestion(AuthenticatedOperation<Question> question);

        [OperationContract]
        void AddAnswer(AuthenticatedOperation<Answer> answer);

        [OperationContract]
        void DeleteAnswer(AuthenticatedOperation<Answer> answer);

        [OperationContract]
        void VoteUpPost(AuthenticatedOperation<IPost> post);

        [OperationContract]
        void VoteDownPost(AuthenticatedOperation<IPost> post);

        [OperationContract]
        void RecommendQuestion(AuthenticatedOperation<Question> question);
    }
}
