using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QueueAndHi.Common;
using QueueAndHi.Common.Notifications;

namespace DAL
{
    internal static class Converter
    {
        internal static user toUser(UserInfo userInfo, UserCredentials userCredentials)
        {
            user u = new user()
            {
                isadmin = Convert.ToByte(userInfo.IsAdmin),
                answer_rankings = null,
                answers = null,
                created = DateTime.Now,
                email = userInfo.EmailAddress,
                ismuted = 0,
                isowner = 0,
                name = userInfo.Username,
                notifications = null,
                pwd = userCredentials.Password,
                question_rankings = null,
                questions = null,
                ranking = 0
            };
            return u;
        }

        internal static UserInfo toExtUser(user user, out UserCredentials userCredentials)
        {
            UserInfo tmpUserInfo = new UserInfo()
            {
                EmailAddress = user.email,
                ID = user.id,
                IsAdmin = Convert.ToBoolean(user.isadmin),
                IsMuted = Convert.ToBoolean(user.ismuted),
                Ranking = user.ranking,
                Username = user.name
            };
            
            userCredentials = new UserCredentials(user.name, user.pwd);
            
            return tmpUserInfo;
        }


        internal static question toQuestion(Question question)
        {
            question intQuestion = new question
            {
                title = question.title,
                contents = question.Content,
                author_id = question.Author.ID,
                created = question.DatePosted,
                recommended = Convert.ToByte(false),
                right_answer_id = question.RightAnswerId
            };
            return intQuestion;
        }

        internal static Question toExtQuestion(question question, UserInfo userInfo, RankingHistory rankingHistory, IEnumerable<string> tags)
        {
            Question extQuestion = new Question()
            {
                Author = userInfo,
                Content = question.contents,
                DatePosted = question.created,
                ID = question.id,
                IsRecommended = Convert.ToBoolean(question.recommended),
                Ranking = rankingHistory,
                RightAnswerId = question.right_answer_id,
                Tags = tags,
                Title = question.title,
                AnswerCount = 0
            };
            return extQuestion;
        }

        internal static answer toAnswer(Answer answer)
        {
            throw new NotImplementedException();
        }

        internal static Answer toExtAnswer(answer answer, UserInfo userInfo, RankingHistory rankingHistory)
        {
            Answer extAnswer = new Answer
            {
                Author = userInfo,
                Content = answer.contents,
                DatePosted = answer.created,
                ID = answer.id,
                Ranking = rankingHistory,
                RelatedQuestionId = answer.question_id
            };

            return extAnswer;
        }

        internal static notification toNotification(Notification notification)
        {
            throw new NotImplementedException();
        }

        internal static Notification toExtNotification(notification notification)
        {
            throw new NotImplementedException();
        }

        internal static RankingHistory toExtRankingHistory(IEnumerable<answer_rankings> answerRankings)
        {

            return new RankingHistory(answerRankings.Select(rank => new RankingEntry(rank.author_id, toExtRankingType(rank.rank))));
        }

        internal static RankingType toExtRankingType(byte rankingType)
        {
            return rankingType == 1 ? RankingType.Up : RankingType.Down;
        }

        internal static byte toRankingType(RankingType rankingType)
        {
            return rankingType == RankingType.Up ? (byte)1 : (byte)0;
        }

        internal static RankingHistory toExtRankingHistory(IEnumerable<question_rankings> questionRankings)
        {
            throw new NotImplementedException();
        }

        internal static IEnumerable<question_rankings> toQuestionRanking(RankingHistory rankingHistory)
        {
            throw new NotImplementedException();
        }

        internal static IEnumerable<answer_rankings> toAnswerRanking(RankingHistory rankingHistory)
        {
            throw new NotImplementedException();
        }

        internal static tag toTag(string tag, int questionId)
        {
            return new tag
            {
                question_id = questionId,
                tag1 = tag
            };
        }
    }
}
