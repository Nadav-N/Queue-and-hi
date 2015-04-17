﻿using QueueAndHi.Common;
using QueueAndHi.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace QueueAndHi.BL
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "PostQueries" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select PostQueries.svc or PostQueries.svc.cs at the Solution Explorer and start debugging.
    public class PostQueries : IPostQueries
    {
        public IEnumerable<Question> FreeSearch(AuthenticatedOperation<string> searchString)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Question> TagsSearch(AuthenticatedOperation<IEnumerable<string>> tags)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Question> GetMyQuestions(AuthenticationToken authToken)
        {
            throw new NotImplementedException();
        }

        public DiscussionThread GetDiscussionThreadById(AuthenticatedOperation<int> id)
        {
            throw new NotImplementedException();
        }
    }
}