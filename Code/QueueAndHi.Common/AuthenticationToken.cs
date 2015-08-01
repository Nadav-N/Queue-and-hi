using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.Common
{
    [DataContract]
    public class AuthenticationToken
    {
        public AuthenticationToken(string tokenString)
        {
            TokenString = tokenString;
        }

       [DataMember]
       public string TokenString { get; private set; }
    }
}
