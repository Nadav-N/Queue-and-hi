﻿using System;
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
            throw new NotImplementedException();
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

        internal static Question toExtQuestion(question question)
        {
            throw new NotImplementedException();
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