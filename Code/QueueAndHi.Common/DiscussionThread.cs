using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QueueAndHi.Common
{
    [DataContract]
    public class DiscussionThread
    {
        [DataMember]
        public Question Question { get; set; }

        [DataMember]
        public IEnumerable<Answer> Answers { get; set; }

        [DataMember]
        public int Version { get; set; }
    }
}
