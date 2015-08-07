using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.Common.Services
{
    [ServiceContract]
    public interface IPostQueries
    {
        [OperationContract]
        IEnumerable<Question> FreeSearch(string searchString);

        [OperationContract]
        IEnumerable<Question> TagsSearch(string tag);

        [OperationContract]
        IEnumerable<Question> GetMyQuestions(AuthenticationToken authToken);

        [OperationContract]
        IEnumerable<Question> GetLatestQuestions();

        [OperationContract]
        DiscussionThread GetDiscussionThreadById(int id);
    }
}
