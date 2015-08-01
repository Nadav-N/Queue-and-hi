using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.Common
{
    [DataContract]
    [KnownType(typeof(Answer))]
    [KnownType(typeof(AuthenticatedIdentity))]
    [KnownType(typeof(AuthenticationToken))]
    [KnownType(typeof(DiscussionThread))]
    [KnownType(typeof(Question))]
    [KnownType(typeof(RankingHistory))]
    [KnownType(typeof(UserCredentials))]
    [KnownType(typeof(UserInfo))]
    public class AuthenticatedOperation<TPayload>
    {
        public AuthenticatedOperation(AuthenticationToken token, TPayload payload)
        {
            Token = token;
            Payload = payload;
        }

        [DataMember]
        public AuthenticationToken Token { get; private set; }

        [DataMember]
        public TPayload Payload { get; private set; }
    }
}
