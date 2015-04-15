using QueueAndHi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QueueAndHi.BL.Authentication
{
    public class AuthTokenSerializer : IAuthTokenSerializer
    {
        public AuthenticationToken Serialize(UserCredentials userCredentials)
        {
            throw new NotImplementedException();
        }

        public UserCredentials Deserialize(AuthenticationToken authToken)
        {
            throw new NotImplementedException();
        }
    }
}