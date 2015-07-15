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
        /// Serializes the user credentials to an auth token, saves it in the cache and returns the token with the user ID.
        /// </summary>
        /// <param name="userCredentials">Credentials of the user</param>
        /// <returns>Auth Token with user ID</returns>
        AuthenticatedUser Serialize(UserCredentials userCredentials);

        /// <summary>
        /// Deserializes authentication token into the user ID of the relevant user
        /// </summary>
        /// <param name="authToken">The authentication token</param>
        /// <returns>User ID of the relevant user</returns>
        int Deserialize(AuthenticationToken authToken);
    }
}
