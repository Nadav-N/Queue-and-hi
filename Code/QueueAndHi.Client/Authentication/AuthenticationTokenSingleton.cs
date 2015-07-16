using QueueAndHi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.Client.Authentication
{
    public class AuthenticationTokenSingleton
    {
        private static AuthenticationTokenSingleton instance;

        private AuthenticationTokenSingleton() { }

        public static AuthenticationTokenSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AuthenticationTokenSingleton();
                }
                return instance;
            }
        }

        public AuthenticatedIdentity AuthenticatedIdentity { get; set; }

        public UserInfo AuthenticatedUser { get; set; }

        public bool IsLoggedIn()
        {
            return AuthenticatedIdentity != null && AuthenticatedIdentity.Token != null;
        }

        public AuthenticatedOperation<T> CreateAuthenticatedOperation<T>(T input)
        {
            return new AuthenticatedOperation<T>(AuthenticatedIdentity.Token, input);
        }
    }
}
