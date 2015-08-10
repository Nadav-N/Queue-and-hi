using DAL;
using QueueAndHi.BL.Authentication;
using QueueAndHi.Common;
using QueueAndHi.Common.Logic.Validations.Question;
using QueueAndHi.Common.Logic.Validators;
using QueueAndHi.Common.Services;
using System;

namespace QueueAndHi.BL
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "PostServices" in code, svc and config ifle together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select PostServices.svc or PostServices.svc.cs at the Solution Explorer and start debugging.
    public class PostServices : IPostServices
    {
        private IAuthTokenSerializer authTokenSerializer;
        private UserOps userOps;
        private IValidator<Question> newQuestionValidator;
        private IValidator<Answer> newAnswerValidator;

        public PostServices()
        {
            this.authTokenSerializer = new AuthTokenSerializer();
            this.userOps = new UserOps();
            this.newQuestionValidator = new TitleValidator(new ContentValidator());
            this.newAnswerValidator = new ContentValidator();
        }

        public void AddQuestion(AuthenticatedOperation<Question> question)
        {
            UserInfo user = GetUserFromRequest(question);
            if (user.IsMuted)
            {
                throw new InvalidOperationException("The user can not add a new question because he is muted.");
            }

            
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

        private UserInfo GetUserFromRequest<T>(AuthenticatedOperation<T> operation)
        {
            int userId = authTokenSerializer.Deserialize(operation.Token);
            return this.userOps.GetUserInfo(userId);
        }
    }
}
