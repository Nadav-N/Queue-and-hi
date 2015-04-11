using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.Common
{
    public class AuthenticatedOperation<TPayload>
    {
        public AuthenticatedOperation(AuthenticationToken token, TPayload payload)
        {
            Token = token;
            Payload = payload;
        }

        public AuthenticationToken Token { get; private set; }

        public TPayload Payload { get; private set; }
    }
}
