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
        Question AddQuestion(AuthenticatedOperation<Question> question);

        [OperationContract]
        void DeleteQuestion(AuthenticatedOperation<int> questionId);

        [OperationContract]
        void AddAnswer(AuthenticatedOperation<Tuple<int, string>> answerData);

        [OperationContract]
        void DeleteAnswer(AuthenticatedOperation<int> answerId);

        [OperationContract]
        void VoteUpQuestion(AuthenticatedOperation<int> questionId);

        [OperationContract]
        void VoteDownQuestion(AuthenticatedOperation<int> questionId);

        [OperationContract]
        void VoteUpAnswer(AuthenticatedOperation<int> answerId);

        [OperationContract]
        void VoteDownAnswer(AuthenticatedOperation<int> answerId);

        [OperationContract]
        void CancelQuestionRanking(AuthenticatedOperation<int> questionId);

        [OperationContract]
        void CancelAnswerVote(AuthenticatedOperation<int> answerId);

        [OperationContract]
        void RecommendQuestion(AuthenticatedOperation<int> questionId);

        [OperationContract]
        void UnrecommendQuestion(AuthenticatedOperation<int> questionId);

        [OperationContract]
        void MarkAsRightAnswer(AuthenticatedOperation<int> answerId);

        [OperationContract]
        void UnmarkAsRightAnswer(AuthenticatedOperation<int> answerId);
    }
}
