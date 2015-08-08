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
            throw new NotImplementedException();
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

        internal static Answer toExtAnswer(answer answer)
        {
            throw new NotImplementedException();
        }

        internal static notification toNotification(Notification notification)
        {
            throw new NotImplementedException();
        }

        internal static Notification toExtNotification(notification notification)
        {
            throw new NotImplementedException();
        }

    }
}
