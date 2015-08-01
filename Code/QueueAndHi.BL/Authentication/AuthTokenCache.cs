using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QueueAndHi.BL.Authentication
{
    public class AuthTokenCache
    {
        private static AuthTokenCache instance;

        private ConcurrentDictionary<string, int> tokenCache = new ConcurrentDictionary<string, int>();

        public static AuthTokenCache Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AuthTokenCache();
                }
                return instance;
            }
       } 

        public ConcurrentDictionary<string, int> TokenCache
        {
            get
            {
                return this.tokenCache;
            }
        }
    }
}