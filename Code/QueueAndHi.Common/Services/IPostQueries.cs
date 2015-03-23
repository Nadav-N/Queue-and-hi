using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.Common.Services
{
    public interface IPostQueries
    {
        [OperationContract]
        IEnumerable<Question> FreeSearch(string searchString);

        [OperationContract]
        IEnumerable<Question> TagsSearch(IEnumerable<string> tags);

        [OperationContract]
        IEnumerable<Question> GetMyQuestions(UserInfo user);

        [OperationContract]
        DiscussionThread GetDiscussionThreadById(int id);
    }
}
