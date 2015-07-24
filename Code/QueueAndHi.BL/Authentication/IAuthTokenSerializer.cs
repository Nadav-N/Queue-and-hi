using QueueAndHi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.BL.Authentication
{
    public interface IAuthTokenSerializer
    {
        /// <summary>
        /// Validates the user credentials, serializes the user credentials to an auth token, saves it in the cache and returns the token with the user ID.
        /// or returns false if the user credentials are not correct.
        /// </summary>
        /// <param name="userCredentials">Credentials of the user</param>
        /// <returns>True if the user was identified successfully</returns>
        bool ValidateAndSerialize(UserCredentials userCredentials, out AuthenticatedIdentity serializedIdentity);

        /// <summary>
        /// Deserializes authentication token into the user ID of the relevant user
        /// </summary>
        /// <param name="authToken">The authentication token</param>
        /// <returns>User ID of the relevant user</returns>
        int Deserialize(AuthenticationToken authToken);
    }
}
