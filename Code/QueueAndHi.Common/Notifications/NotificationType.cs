using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.Common.Notifications
{
    public enum NotificationType
    {
        NewAnswer,
        QuestionRankedUp,
        QuestionRankedDown,
        AnswerRankedUp,
        AnswerRankedDown,
        AnswerMarkedAsRight,
        QuestionMarkedAsRecommended,
        QuestionMarkedAsUnrecommended,
        PostWasDeleted,
        YouWereMuted,
        YouWereUnmuted,
        MarkedAsLecturer,
        UnmarkedAsLecturer
    }
}
