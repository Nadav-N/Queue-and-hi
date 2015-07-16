using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.Common
{
    public class AuthenticatedIdentity
    {
        public int UserID { get; set; }
        public AuthenticationToken Token { get; set; }
    }
}
