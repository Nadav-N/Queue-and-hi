using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.Common
{
    public class AuthenticatedUser
    {
        public UserInfo User { get; set; }
        public AuthenticationToken Token { get; set; }
    }
}
