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
        void AddQuestion(Question question);

        [OperationContract]
        void DeleteQuestion(Question question);

        [OperationContract]
        void AddAnswer(Answer answer);

        [OperationContract]
        void DeleteAnswer(Answer answer);

        [OperationContract]
        void VoteUpPost(IPost post);

        [OperationContract]
        void VoteDownPost(IPost post);

        [OperationContract]
        void RecommendQuestion(Question question);
    }
}
