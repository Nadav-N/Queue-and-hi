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

        public AuthenticatedIdentity AuthenticatedIdentity { get; private set; }

        public UserInfo AuthenticatedUser { get; private set; }

        public void LogIn(AuthenticatedIdentity authenticatedIdentity, UserInfo authenticatedUser)
        {
            AuthenticatedIdentity = authenticatedIdentity;
            AuthenticatedUser = authenticatedUser;

            OnLogIn();
        }

        public void LogOut()
        {
            OnLogOut();

            AuthenticatedIdentity = null;
            AuthenticatedUser = null;
        }

        public bool IsLoggedIn
        {
            get
            {
                return AuthenticatedIdentity != null && AuthenticatedIdentity.Token != null;
            }
        }

        public AuthenticatedOperation<T> CreateAuthenticatedOperation<T>(T input)
        {
            return new AuthenticatedOperation<T>(AuthenticatedIdentity.Token, input);
        }

        private void OnLogOut()
        {
            if (UserLoggedOut != null)
            {
                UserLoggedOut(this, EventArgs.Empty);
            }
        }

        public event EventHandler<EventArgs> UserLoggedOut;

        private void OnLogIn()
        {
            if (UserLoggedIn != null)
            {
                UserLoggedIn(this, EventArgs.Empty);
            }
        }

        public event EventHandler<EventArgs> UserLoggedIn;
    }
}
