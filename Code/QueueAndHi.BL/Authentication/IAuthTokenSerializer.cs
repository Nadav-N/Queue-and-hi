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
        AuthenticatedUser Serialize(UserCredentials userCredentials);
        int Deserialize(AuthenticationToken authToken);
    }
}
