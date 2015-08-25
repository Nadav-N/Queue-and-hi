using DAL;
using QueueAndHi.BL.Authentication;
using QueueAndHi.Common;
using QueueAndHi.Common.Logic.Validations.Question;
using QueueAndHi.Common.Logic.Validations.User;
using QueueAndHi.Common.Logic.Validators;
using QueueAndHi.Common.Notifications;
using QueueAndHi.Common.Services;
using System;
using System.Collections.Generic;

namespace QueueAndHi.BL
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "PostServices" in code, svc and config ifle together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select PostServices.svc or PostServices.svc.cs at the Solution Explorer and start debugging.
    public class PostServices : IPostServices
    {
        private IAuthTokenSerializer authTokenSerializer;
        private UserOps userOps;
        private PostOps postOps;
        private NotificationOps notificationOps;
        private IValidator<Question> newQuestionValidator;
        private IValidator<Answer> newAnswerValidator;
        private IValidator<UserInfo> recommendPostValidator;
        private IValidator<UserInfo> rankDownValidator;

        public PostServices()
        {
            this.authTokenSerializer = new AuthTokenSerializer();
            this.userOps = new UserOps();
            this.notificationOps = new NotificationOps();
            this.newQuestionValidator = new TitleValidator(new ContentValidator());
            this.newAnswerValidator = new ContentValidator();
            this.recommendPostValidator = new RecommendQuestionValidator();
            this.rankDownValidator = new RankDownValidator();
            this.postOps = new PostOps();
        }

        public Question AddQuestion(AuthenticatedOperation<Question> question)
        {
            UserInfo user = GetUserFromRequest(question);
            if (user.IsMuted)
            {
                throw new InvalidOperationException("The user can not add a new question because he is muted.");
            }

            question.Payload.IsRecommended = false;
            question.Payload.RightAnswerId = null;
            question.Payload.AnswerCount = 0;
            question.Payload.Author = user;
            question.Payload.DatePosted = DateTime.Now;

            OperationResult validationResult = this.newQuestionValidator.IsValid(question.Payload);
            if (!validationResult.IsSuccessful)
            {
                throw new ArgumentException(String.Join("\n", validationResult.ErrorMessages));
            }

            return this.postOps.AddQuestion(question.Payload);
        }

        public void DeleteQuestion(AuthenticatedOperation<int> questionId)
        {
            UserInfo user = GetUserFromRequest(questionId);
            Question questionToDelete = this.postOps.GetQuestionById(questionId.Payload);
            if (questionToDelete.Author.ID != user.ID && !user.IsAdmin)
            {
                throw new InvalidOperationException("Only the author of the post or an admin can delete the post.");
            }

            this.postOps.DeleteQuestion(questionId.Payload);

            this.notificationOps.SaveNotification(questionToDelete.Author.ID,
                string.Format("Your question: \"{0}\" has been deleted.", questionToDelete.Title),
                NotificationType.PostWasDeleted);
        }

        public void AddAnswer(AuthenticationToken token, int questionId, string content)
        {
            UserInfo user = GetUserFromRequest(token);
            if (user.IsMuted)
            {
                throw new InvalidOperationException("The user can not add a new question because he is muted.");
            }

            Answer newAnswer = new Answer
            {
                Content = content,
                RelatedQuestionId = questionId,
                Author = user,
                DatePosted = DateTime.Now
            };

            OperationResult validationResult = this.newAnswerValidator.IsValid(newAnswer);
            if (!validationResult.IsSuccessful)
            {
                throw new ArgumentException(String.Join("\n", validationResult.ErrorMessages));
            }

            this.postOps.AddAnswer(newAnswer);
            this.postOps.IncrementVersion(questionId);

            Question relatedQuestion = this.postOps.GetQuestionById(questionId);
            this.notificationOps.SaveNotification(relatedQuestion.Author.ID,
                string.Format("An answer was added to your question \"{0}\"", relatedQuestion.Title),
                NotificationType.NewAnswer);
        }

        public void DeleteAnswer(AuthenticatedOperation<int> answerId)
        {
            UserInfo user = GetUserFromRequest(answerId);
            Answer answerToDelete = this.postOps.GetAnswerById(answerId.Payload);
            if (answerToDelete.Author.ID != user.ID && !user.IsAdmin)
            {
                throw new InvalidOperationException("Only the author of the post or an admin can delete the post.");
            }

            this.postOps.DeleteAnswer(answerId.Payload);
            this.postOps.IncrementVersion(answerToDelete.RelatedQuestionId);

            Question relatedQuestion = this.postOps.GetQuestionById(answerToDelete.RelatedQuestionId);
            this.notificationOps.SaveNotification(answerToDelete.Author.ID,
                string.Format("Your answer to the question: \"{0}\" has been deleted.", relatedQuestion.Title),
                NotificationType.PostWasDeleted);
        }

        public void RecommendQuestion(AuthenticatedOperation<int> questionId)
        {
            UserInfo user = GetUserFromRequest(questionId);
            OperationResult validationResult = this.recommendPostValidator.IsValid(user);
            if (!validationResult.IsSuccessful)
            {
                throw new InvalidOperationException(String.Join("\n", validationResult.ErrorMessages));
            }

            this.postOps.RecommendQuestion(questionId.Payload);
            this.postOps.IncrementVersion(questionId.Payload);

            Question question = this.postOps.GetQuestionById(questionId.Payload);
            this.notificationOps.SaveNotification(question.Author.ID,
                string.Format("Your question \"{0}\" has been marked as recommeneded.", question.Title),
                NotificationType.QuestionMarkedAsRecommended);
        }

        public void UnrecommendQuestion(AuthenticatedOperation<int> questionId)
        {
            UserInfo user = GetUserFromRequest(questionId);
            OperationResult validationResult = this.recommendPostValidator.IsValid(user);
            if (!validationResult.IsSuccessful)
            {
                throw new InvalidOperationException(String.Join("\n", validationResult.ErrorMessages));
            }

            this.postOps.UnrecommendQuestion(questionId.Payload);
            this.postOps.IncrementVersion(questionId.Payload);

            Question question = this.postOps.GetQuestionById(questionId.Payload);
            this.notificationOps.SaveNotification(question.Author.ID,
                string.Format("Your question \"{0}\" has been marked as unrecommeneded.", question.Title),
                NotificationType.QuestionMarkedAsUnrecommended);
        }

        public void VoteUpQuestion(AuthenticatedOperation<int> questionId)
        {
            UserInfo user = GetUserFromRequest(questionId);
            Question question = this.postOps.GetQuestionById(questionId.Payload);
            if (question.Author.ID == user.ID)
            {
                throw new InvalidOperationException("User can not rank his own posts.");
            }

            this.postOps.RankUpQuestion(questionId.Payload, user.ID);
            this.postOps.IncrementVersion(questionId.Payload);

            this.notificationOps.SaveNotification(question.Author.ID,
                string.Format("Your question \"{0}\" has been ranked up", question.Title),
                NotificationType.QuestionRankedUp);
        }

        public void VoteDownQuestion(AuthenticatedOperation<int> questionId)
        {
            UserInfo user = GetUserFromRequest(questionId);
            Question question = this.postOps.GetQuestionById(questionId.Payload);
            if (question.Author.ID == user.ID)
            {
                throw new InvalidOperationException("User can not rank his own posts.");
            }

            OperationResult validationResult = this.rankDownValidator.IsValid(user);
            if (!validationResult.IsSuccessful)
            {
                throw new InvalidOperationException(String.Join("\n", validationResult.ErrorMessages));
            }

            //if(question.Ranking.Count != 0 && question.Ranking
            this.postOps.RankDownQuestion(questionId.Payload, user.ID);
            this.postOps.IncrementVersion(questionId.Payload);

            this.notificationOps.SaveNotification(question.Author.ID,
                string.Format("Your question \"{0}\" has been ranked down", question.Title),
                NotificationType.QuestionRankedDown);
        }

        public void VoteUpAnswer(AuthenticatedOperation<int> answerId)
        {
            UserInfo user = GetUserFromRequest(answerId);
            Answer answer = this.postOps.GetAnswerById(answerId.Payload);
            if (answer.Author.ID == user.ID)
            {
                throw new InvalidOperationException("User can not rank his own posts.");
            }

            //check if the user is canceling a rank down or simply adding a rank up
            if (answer.Ranking.Contains(user.ID, RankingType.Down))
            {
                CancelAnswerVote(answerId);
            }
            else
            {
                this.postOps.RankUpAnswer(answerId.Payload, user.ID);
                this.postOps.IncrementVersion(answer.RelatedQuestionId);
            }
            Question question = this.postOps.GetQuestionById(answer.RelatedQuestionId);
            this.notificationOps.SaveNotification(answer.Author.ID,
                string.Format("Your answer to the question \"{0}\" has been ranked up", question.Title),
                NotificationType.AnswerRankedUp);
        }

        public void VoteDownAnswer(AuthenticatedOperation<int> answerId)
        {
            UserInfo user = GetUserFromRequest(answerId);
            Answer answer = this.postOps.GetAnswerById(answerId.Payload);
            if (answer.Author.ID == user.ID)
            {
                throw new InvalidOperationException("User can not rank his own posts.");
            }

            OperationResult validationResult = this.rankDownValidator.IsValid(user);
            if (!validationResult.IsSuccessful)
            {
                throw new InvalidOperationException(String.Join("\n", validationResult.ErrorMessages));
            }

            //check if the user is canceling a rank up or simply adding a rank down
            if (answer.Ranking.Contains(user.ID, RankingType.Up))
            {
                CancelAnswerVote(answerId);
            }
            else
            {
                this.postOps.RankDownAnswer(answerId.Payload, user.ID);
                this.postOps.IncrementVersion(answer.RelatedQuestionId);
            }
            Question question = this.postOps.GetQuestionById(answer.RelatedQuestionId);
            this.notificationOps.SaveNotification(answer.Author.ID,
                string.Format("Your answer to the question \"{0}\" has been ranked down", question.Title),
                NotificationType.AnswerRankedDown);
        }

        public void CancelQuestionRanking(AuthenticatedOperation<int> questionId)
        {
            UserInfo user = GetUserFromRequest(questionId);
            Question question = this.postOps.GetQuestionById(questionId.Payload);
            if (question.Author.ID == user.ID)
            {
                throw new InvalidOperationException("User can not rank his own posts.");
            }

            this.postOps.CancelQuestionRank(questionId.Payload, user.ID);
            this.postOps.IncrementVersion(questionId.Payload);
        }

        public void CancelAnswerVote(AuthenticatedOperation<int> answerId)
        {
            UserInfo user = GetUserFromRequest(answerId);
            Answer answer = this.postOps.GetAnswerById(answerId.Payload);
            if (answer.Author.ID == user.ID)
            {
                throw new InvalidOperationException("User can not rank his own posts.");
            }

            this.postOps.CancelAnswerRanking(answerId.Payload, user.ID);
            this.postOps.IncrementVersion(answer.RelatedQuestionId);
        }

        public void MarkAsRightAnswer(AuthenticatedOperation<int> answerId)
        {
            UserInfo user = GetUserFromRequest(answerId);
            Answer answer = this.postOps.GetAnswerById(answerId.Payload);
            Question relatedQuestion = this.postOps.GetQuestionById(answer.RelatedQuestionId);
            if (relatedQuestion.Author.ID != user.ID && !user.IsAdmin)
            {
                throw new InvalidOperationException("Only the question owner or an admin can mark an answer as right.");
            }
            if (relatedQuestion.RightAnswerId.HasValue)
            {
                this.postOps.UnmarkAsRightAnswer(relatedQuestion.RightAnswerId.Value);
                this.postOps.IncrementVersion(answer.RelatedQuestionId);
            }

            this.postOps.MarkAsRightAnswer(answerId.Payload);
            this.postOps.IncrementVersion(answer.RelatedQuestionId);

            this.notificationOps.SaveNotification(answer.Author.ID,
                string.Format("Your answer to the question \"{0}\" has been marked as the right answer.", relatedQuestion.Title),
                NotificationType.AnswerMarkedAsRight);
        }

        public void UnmarkAsRightAnswer(AuthenticatedOperation<int> answerId)
        {
            UserInfo user = GetUserFromRequest(answerId);
            Answer answer = this.postOps.GetAnswerById(answerId.Payload);
            Question relatedQuestion = this.postOps.GetQuestionById(answer.RelatedQuestionId);
            if (relatedQuestion.Author.ID != user.ID && !user.IsAdmin)
            {
                throw new InvalidOperationException("Only the question owner or an admin can unmark an answer from right.");
            }

            this.postOps.UnmarkAsRightAnswer(answerId.Payload);
            this.postOps.IncrementVersion(answer.RelatedQuestionId);
        }

        private UserInfo GetUserFromRequest<T>(AuthenticatedOperation<T> operation)
        {
            return GetUserFromRequest(operation.Token);
        }

        private UserInfo GetUserFromRequest(AuthenticationToken token)
        {
            int userId = authTokenSerializer.Deserialize(token);
            return this.userOps.GetUserInfo(userId);
        }
    }
}
