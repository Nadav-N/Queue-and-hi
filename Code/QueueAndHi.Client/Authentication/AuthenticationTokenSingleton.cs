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

        public AuthenticatedUser AuthenticatedUser { get; set; }
    }
}
