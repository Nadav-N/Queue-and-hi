using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QueueAndHi.BL.Authentication
{
    public class AuthTokenCache
    {
        ConcurrentBag<string> tokenCache = new ConcurrentBag<string>();

        public ConcurrentBag<string> TokenCache
        {
            get
            {
                return this.tokenCache;
            }
        }
    }
}