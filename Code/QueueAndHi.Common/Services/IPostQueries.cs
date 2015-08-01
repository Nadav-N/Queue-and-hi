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
        IEnumerable<Question> FreeSearch(AuthenticatedOperation<string> searchString);

        [OperationContract]
        IEnumerable<Question> TagsSearch(AuthenticatedOperation<IEnumerable<string>> tags);

        [OperationContract]
        IEnumerable<Question> GetMyQuestions(AuthenticationToken authToken);

        [OperationContract]
        DiscussionThread GetDiscussionThreadById(AuthenticatedOperation<int> id);
    }
}
