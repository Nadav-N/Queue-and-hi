using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QueueAndHi.BL.Authentication
{
    public class AuthTokenCache
    {
        ConcurrentDictionary<string, int> tokenCache = new ConcurrentDictionary<string, int>();

        public ConcurrentDictionary<string, int> TokenCache
        {
            get
            {
                return this.tokenCache;
            }
        }
    }
}