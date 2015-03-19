using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.Common
{
    [DataContract]
    public interface IPost
    {
        [DataMember]
        public string Content { get; set; }

        [DataMember]
        public int Ranking { get; set; }

        [DataMember]
        public UserInfo Author { get; set; }
    }
}
