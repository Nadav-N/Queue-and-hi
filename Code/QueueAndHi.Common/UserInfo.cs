using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.Common
{
    [DataContract]
    public class UserInfo
    {
        [DataMember]
        public int Ranking { get; set; }

        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public string EmailAddress { get; set; }

        [DataMember]
        public bool IsAdmin { get; set; }

        [DataMember]
        public bool IsMuted { get; set; }

        [DataMember]
        public int ID { get; set; }
    }
}
