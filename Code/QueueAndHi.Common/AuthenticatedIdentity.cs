using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.Common
{
    [DataContract]
    public class AuthenticatedIdentity
    {
        [DataMember]
        public int UserID { get; set; }

        [DataMember]
        public AuthenticationToken Token { get; set; }
    }
}
